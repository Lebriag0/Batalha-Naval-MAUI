# Batalha Naval em .NET MAUI (V1.0.0)

Este é um projeto de um jogo de **Batalha Naval** desenvolvido em **.NET MAUI**. O objetivo do jogo é destruir os navios do oponente, alternando turnos entre o jogador esquerdo e o jogador direito. O tabuleiro é unificado, com navios posicionados automaticamente para ambos os lados.

## Funcionalidades

- Posicionamento automático de navios para ambos os lados.
- Interface gráfica de tabuleiro com botões coloridos.
- Exibição temporária dos navios antes de iniciar o jogo.
- Verificação de vitória ao destruir todos os navios do oponente.
- Reinício automático com mensagem de vitória.
- Coluna central pintada de preto como separação entre os lados.

## Como rodar o projeto

### Pré-requisitos

- .NET 7.0 ou superior instalado.
- Ferramentas de desenvolvimento .NET MAUI configuradas.
- Visual Studio 2022 (ou outra IDE que suporte MAUI).

### Instalação

1. Clone o repositório em sua máquina local:
    ```bash
    git clone https://github.com/seu-usuario/Batalha-Naval-MAUI.git
    ```

2. Abra o projeto no **Visual Studio**.

3. Compile e execute o projeto no simulador ou em um dispositivo físico.

### Jogabilidade

- **Clique nos botões do lado esquerdo ou direito** para atacar os navios do oponente.
- Navios atingidos ficarão com a cor **vermelha**, enquanto áreas sem navios ficarão **azuis**.
- O jogo exibe uma mensagem quando todos os navios de um lado forem destruídos.
- Clique no botão de "Reiniciar" para começar uma nova partida.

## Estrutura do Projeto

- **MainPage.xaml**: Define a interface gráfica e a disposição dos botões do tabuleiro.
- **MainPage.xaml.cs**: Contém a lógica principal do jogo, incluindo posicionamento dos navios, cliques nos botões, e verificação de vitória.
- **Botões de Jogo**: O tabuleiro usa uma grade unificada com separação por colunas para definir os lados esquerdo e direito.
- **Posicionamento Automático de Navios**: Cada lado do tabuleiro recebe 5 navios de tamanhos variados, posicionados de forma aleatória.

## Melhorias Futuras

- Adicionar modo multiplayer online.
- Implementar diferentes níveis de dificuldade para posicionamento dos navios.
- Adicionar animações e efeitos sonoros.

## Contribuição

Sinta-se à vontade para contribuir com este projeto! Basta seguir os seguintes passos:

1. Faça um fork do repositório.
2. Crie uma nova branch:
    ```bash
    git checkout -b feature/sua-feature
    ```
3. Faça suas modificações e faça commit delas:
    ```bash
    git commit -m 'Adiciona nova funcionalidade'
    ```
4. Envie para a branch principal:
    ```bash
    git push origin feature/sua-feature
    ```
5. Abra um Pull Request.

## Licença

Este projeto está sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais informações.
