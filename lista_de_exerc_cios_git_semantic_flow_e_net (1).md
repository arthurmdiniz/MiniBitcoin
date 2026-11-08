# Garantia da Qualidade de Software
## Gestão e Qualidade de Software
### Professor Daniel Henrique Matos de Paiva

**Ánima Educação**

---

## Lista de Exercícios

Esta lista de exercício deve:
- Ser realizada em equipes de até 06 alunos.
- Ser entregue no prazo proposto.
- Todos os alunos da equipe devem entregar os exercícios propostos. Entregas individuais.

---

### 1. Configuração Inicial do Ambiente

Abra o seu terminal (ou o terminal integrado do Visual Studio Code) e siga os passos abaixo para estruturar a nossa solução, os projetos e o repositório Git.

#### 1.1. Inicialização do Repositório Git
```bash
mkdir MiniBitcoin
cd MiniBitcoin
git init
```
*(Ou crie direto no GitHub o repositório e clone para a sua máquina).*

#### 1.2. Criação da Solução e dos Projetos (.NET)
Vamos criar uma solução limpa contendo o projeto principal de console e o projeto de testes unitários:

```bash
# Criar o arquivo de solução (.sln)
dotnet new sln -n MiniBitcoin

# Criar o projeto de console (Aplicação)
dotnet new console -n MiniBitcoin.Console

# Criar o projeto de testes unitários (xUnit)
dotnet new xunit -n MiniBitcoin.Tests

# Adicionar os projetos à solução
dotnet sln add MiniBitcoin.Console/MiniBitcoin.Console.csproj
dotnet sln add MiniBitcoin.Tests/MiniBitcoin.Tests.csproj

# Referenciar o projeto principal dentro do projeto de testes
dotnet add MiniBitcoin.Tests/MiniBitcoin.Tests.csproj reference MiniBitcoin.Console/MiniBitcoin.Console.csproj
```

#### 1.3. Primeiro Commit (Estrutura Inicial)
Faça o rastreamento dos arquivos criados e realize o primeiro commit seguindo estritamente o padrão *Conventional Commits*:

```bash
git add .
git commit -m "chore: initial project structure with console and xunit apps"
```

---

### 2. Requisitos Funcionais da Aplicação

A aplicação de console deverá permitir:

1. **Gestão de Carteira (Wallet):** Gerenciamento de saldo em moeda fiduciária (BRL) e saldo em Satoshis/Bitcoins, com validações para evitar saldos negativos em operações.
2. **Cotação e Simulação de Mercado:** Manutenção e consulta do preço atual do Bitcoin (simulado ou atualizável no sistema).
3. **Compra e Venda de Bitcoin:** Executar ordens de compra e venda de Bitcoin utilizando o saldo em BRL, validando fundos disponíveis e atualizando os saldos correspondentes.
4. **Extrato de Transações:** Exibir o histórico completo de operações realizadas (compras, vendas) e o patrimônio total consolidado.

---

### 3. Roteiro de Desenvolvimento e Padrão de Commits

Para cada etapa concluída, você deve criar testes unitários correspondentes antes de implementar a funcionalidade (TDD básico) e utilizar rigorosamente o padrão *Conventional Commits* (`feat`, `fix`, `test`, `refactor`, `docs`, `chore`).

#### Etapa 1: Regras de Negócio e Testes para a Carteira
- **O que fazer:** Crie a entidade `Carteira` (`Wallet`) contendo os saldos iniciais de BRL e Bitcoin. Escreva testes unitários no projeto xUnit garantindo que não seja possível iniciar com valores negativos ou efetuar saques/débitos inválidos.
- **Comandos sugeridos:**
  ```bash
  dotnet test
  git add .
  git commit -m "test(carteira): add unit tests for wallet balance validation"
  git commit -m "feat(carteira): implement wallet entity and initial balance rules"
  ```

#### Etapa 2: Lógica de Compra e Venda de Bitcoin
- **O que fazer:** Implemente a lógica de conversão e transação baseada na cotação atual do Bitcoin. Valide se há saldo em BRL suficiente para compra de frações de BTC e saldo em BTC suficiente para venda. Escreva os testes unitários para cobrir cenários de sucesso e tentativas de transações inválidas.
- **Comandos sugeridos:**
  ```bash
  dotnet test
  git add .
  git commit -m "test(transacao): add unit tests for bitcoin buy and sell operations"
  git commit -m "feat(transacao): implement bitcoin purchase and sale logic with quote validation"
  ```

#### Etapa 3: Histórico de Extrato e Gestão de Portfólio
- **O que fazer:** Implemente a classe de Extrato/Histórico para registrar cada operação de compra e venda realizada, além de calcular o patrimônio total consolidado da carteira em BRL com base na cotação atual. Escreva testes unitários cobrindo o fluxo do extrato.
- **Comandos sugeridos:**
  ```bash
  dotnet test
  git add .
  git commit -m "test(extrato): add unit tests for transaction ledger and portfolio valuation"
  git commit -m "feat(extrato): implement transaction history and portfolio summary"
  ```

#### Etapa 4: Interface de Console (`Program.cs`)
- **O que fazer:** Desenvolva o menu interativo no arquivo `Program.cs` para que o usuário possa interagir com a carteira via linha de comando (consultar cotação, ver saldos, comprar Bitcoin, vender Bitcoin e exibir extrato).
- **Comandos sugeridos:**
  ```bash
  dotnet run --project MiniBitcoin.Console/MiniBitcoin.Console.csproj
  git add .
  git commit -m "feat(console): implement interactive menu for bitcoin wallet application"
  ```

#### Etapa 5: Refatoração e Documentação (`README.md`)
- **O que fazer:** Limpe o código, garanta que todos os testes passem (`dotnet test`) e crie um arquivo `README.md` bem estruturado explicando a arquitetura do projeto e como executá-lo.
- **Comandos sugeridos:**
  ```bash
  dotnet test
  git add .
  git commit -m "refactor(core): improve code readability and organize namespaces"
  git commit -m "docs: add comprehensive instructions in README.md"
  ```

---

### 4. Entrega Final

1. Crie um repositório público no seu GitHub.
2. Conecte o seu repositório local ao remoto e envie o código:
   ```bash
   git remote add origin <URL_DO_SEU_REPOSITORIO_GITHUB>
   git branch -M main
   git push -u origin main
   ```

Verifique se o seu histórico de commits está limpo, organizado e totalmente aderente ao padrão *Conventional Commits* executando:
```bash
git log --oneline
```

*Bom trabalho a todos!*