using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace PAM___Batalha_naval
{
    public partial class MainPage : ContentPage
    {
        private const int Linhas = 11; // Quantidade de linhas do tabuleiro
        private const int Colunas = 9;  // Quantidade de colunas, separadas ao meio
        private Button[] _botoes = new Button[Colunas * Linhas]; // Um único tabuleiro com todos os botões
        private bool[] _navios = new bool[Colunas * Linhas]; // Estado dos navios
        private int _naviosEsquerdoRestantes = 1;
        private int _naviosDireitoRestantes = 1;
        private Random _random = new Random();
        private bool _isJogoEmAndamento = false;

        public MainPage()
        {
            InitializeComponent();
            InicializarTabuleiroUnificado(); // Inicializa o tabuleiro unificado
            PosicionarNaviosAutomaticamente(); // Posiciona os navios de forma automática
            IniciarTemporizadores(); // Inicia a visualização temporária dos navios
        }

        private void InicializarTabuleiroUnificado()
        {
            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                {
                    int indice = i * Colunas + j;
                    _botoes[indice] = new Button
                    {
                        BackgroundColor = Colors.White,
                        Text = ""
                    };

                    // Definir os lados esquerdo e direito
                    if (j == 4)
                    {
                        _botoes[indice].BackgroundColor = Colors.Black; // Pinta a coluna central de preto
                        _botoes[indice].IsEnabled = false; // Desativa os botões na coluna central
                    }
                    else if (j <= 3) // Lado esquerdo
                    {
                        _botoes[indice].Clicked += OnBotaoEsquerdoClick;
                    }
                    else if (j >= 5) // Lado direito
                    {
                        _botoes[indice].Clicked += OnBotaoDireitoClick;
                    }

                    gridTabuleiro.Add(_botoes[indice], j, i); // Adiciona o botão à grid no XAML
                }
            }
        }

        private void PosicionarNaviosAutomaticamente()
        {
            _naviosEsquerdoRestantes = PosicionarNavios(0, 5, 5); // Navios no lado esquerdo (colunas 0 a 3)
            _naviosDireitoRestantes = PosicionarNavios(5, 9, 5); // Navios no lado direito (colunas 5 a 8)
        }

        private int PosicionarNavios(int colInicio, int colFim, int quantidadeNavios)
        {
            int naviosRestantes = 0;
            for (int navio = 0; navio < quantidadeNavios; navio++)
            {
                int tamanhoNavio = _random.Next(2, 4); // Tamanho entre 2 e 4
                bool colocado = false;

                while (!colocado)
                {
                    int linhaInicial = _random.Next(0, Linhas);
                    int colunaInicial = _random.Next(colInicio, colFim); // Restringe entre as colunas do lado

                    if (PodeColocarNavio(linhaInicial, colunaInicial, tamanhoNavio, colFim))
                    {
                        ColocarNavio(linhaInicial, colunaInicial, tamanhoNavio);
                        colocado = true;
                        naviosRestantes++;
                    }
                }
            }
            return naviosRestantes;
        }

        private bool PodeColocarNavio(int linha, int coluna, int tamanho, int colFim)
        {
            if (coluna + tamanho > colFim) return false; // Verifica se o navio cabe horizontalmente

            for (int i = 0; i < tamanho; i++)
            {
                if (_navios[linha * Colunas + (coluna + i)]) return false; // Verifica se o local já está ocupado
            }

            return true;
        }

        private void ColocarNavio(int linha, int coluna, int tamanho)
        {
            for (int i = 0; i < tamanho; i++)
            {
                _navios[linha * Colunas + (coluna + i)] = true; // Marca a posição do navio
            }
        }

        // Lida com os cliques nos botões do lado esquerdo
        private void OnBotaoEsquerdoClick(object sender, EventArgs e)
        {
            if (!_isJogoEmAndamento) return;

            Button botao = (Button)sender;
            int indice = Array.IndexOf(_botoes, botao);

            if (_navios[indice])
            {
                botao.BackgroundColor = Colors.Red;
                _naviosEsquerdoRestantes--;
            }
            else
            {
                botao.BackgroundColor = Colors.Blue;
            }

            VerificarVitoria();
        }

        // Lida com os cliques nos botões do lado direito
        private void OnBotaoDireitoClick(object sender, EventArgs e)
        {
            if (!_isJogoEmAndamento) return;

            Button botao = (Button)sender;
            int indice = Array.IndexOf(_botoes, botao);

            if (_navios[indice])
            {
                botao.BackgroundColor = Colors.Red;
                _naviosDireitoRestantes--;
            }
            else
            {
                botao.BackgroundColor = Colors.Blue;
            }

            VerificarVitoria();
        }

        private async void VerificarVitoria()
        {
            if (_naviosEsquerdoRestantes == 0)
            {
                bool reiniciar = await DisplayAlert("Vitória", "Os navios do lado esquerdo foram destruídos!", "Reiniciar", "Cancelar");
                if (reiniciar) ReiniciarJogo();
                else _isJogoEmAndamento = false;
            }
            else if (_naviosDireitoRestantes == 0)
            {
                bool reiniciar = await DisplayAlert("Vitória", "Os navios do lado direito foram destruídos!", "Reiniciar", "Cancelar");
                if (reiniciar) ReiniciarJogo();
                else _isJogoEmAndamento = false;
            }
        }

        private void ReiniciarJogo()
        {
            // Reinicializa os estados dos botões e dos navios
            Array.Clear(_navios, 0, _navios.Length);
            _naviosEsquerdoRestantes = 5;
            _naviosDireitoRestantes = 5;
            _isJogoEmAndamento = false;

            // Atualiza os botões no tabuleiro
            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                {
                    int indice = i * Colunas + j;

                    // Verifica se é a coluna central
                    if (j == 4)
                    {
                        _botoes[indice].BackgroundColor = Colors.Black; // Pinta a coluna central de preto
                        _botoes[indice].IsEnabled = false; // Desativa os botões na coluna central
                    }
                    else
                    {
                        _botoes[indice].BackgroundColor = Colors.White; // Reseta a cor dos outros botões
                        _botoes[indice].IsEnabled = true; // Ativa os botões
                    }
                }
            }

            // Reposiciona os navios apenas ao iniciar o jogo pela primeira vez
            if (!_isJogoEmAndamento)
            {
                PosicionarNaviosAutomaticamente();
                IniciarTemporizadores();
            }
        }
    

        private async void IniciarTemporizadores()
        {
            // Exibe os navios do lado esquerdo por 3 segundos
            MostrarNavios(0, 4);
            await Task.Delay(3000);
            OcultarNavios(0, 4);

            // Exibe os navios do lado direito por 3 segundos
            MostrarNavios(5, 9);
            await Task.Delay(3000);
            OcultarNavios(5, 9);

            _isJogoEmAndamento = true; // Inicia o jogo
        }

        private void MostrarNavios(int colInicio, int colFim)
        {
            for (int i = 0; i < Linhas; i++)
            {
                for (int j = colInicio; j < colFim; j++)
                {
                    int indice = i * Colunas + j;
                    if (_navios[indice])
                    {
                        _botoes[indice].BackgroundColor = Colors.Gray;
                    }
                }
            }
        }

        private void OcultarNavios(int colInicio, int colFim)
        {
            for (int i = 0; i < Linhas; i++)
            {
                for (int j = colInicio; j < colFim; j++)
                {
                    int indice = i * Colunas + j;
                    _botoes[indice].BackgroundColor = Colors.White;
                }
            }
        }
    }
}
