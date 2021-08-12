# Ewave-favo-de-mel-arquiteto
Desenvolvimento da solução 'Favo de Mel' para concorrer a vaga de Arquiteto de Software - Ewave 2021

# Visão Geral da Aplicação ‘Favo de Mel’

A proposta inicial foi desenvolver uma solução que ajude o restaurante ‘Favo de Mel’ a gerenciar o atendimento ao cliente, pois o mesmo está tendo sérios problemas com isso. Os problemas são: pedidos são feitos e muitas vezes o mesmo não chegam cozinha, clientes cancelam pedido e a cozinha não recebe o aviso e acaba preparando o mesmo, os pedidos estão demorando para serem entregues ou muitas vezes estão entregando pedido fora de ordem sem priorização.
Como esse fluxo hoje é manual e devido a correria dos funcionários para tentar atender os clientes, a comunicação entre eles acaba sendo ineficiente, causando esses gargalos.
Para resolver os principais problemas foi solicitado a criação de uma nova ferramenta que atenda no mínimo os requisitos abaixo:
- Garçom: visualizar comandas abertas, abrir comanda, adicionar pedido a comanda, cancelar pedido da comanda, acompanhar o status de um pedido na cozinha e fechar a comanda;
- Cozinha: visualizar, receber e entregar o pedido pronto para o garçom.

Além dos requisitos mínimos acima, deixamos por opção livre a implementação de alguns requisitos que seriam interessantes para o restaurante, são eles:
-  Notificação ativa entre o garçom e a cozinha ou vice-versa;
-  Garçom poder visualizar o andamento de preparo dos pedidos de uma comanda;
-  Repriorização de ordem de preparo dos pedidos pela cozinha.

# Pré-requisitos para utilização:
- Docker (https://docs.docker.com/docker-for-windows/install/)
- .NET Core 5 (https://dotnet.microsoft.com/download/dotnet/5.0 )

- Entrar na pastar src e executar o comando:
docker-compose -f 'docker-compose.yml' -f 'docker-compose.override.yml' up -d --build

- No Visual Studio:
Marcar o projeto docker-compose com "Projeto Inicial" e executar a build (conforme exibido na imagem abaixo)
![image](https://user-images.githubusercontent.com/19453244/129209793-73e1f907-d70b-4e3a-887d-12693404b51c.png)

# Visão Geral da Arquitetura

Esta aplicação é cross-pataform e apresenta uma proposta inicial da solução ‘Favo de Mel’ no qual tem a principal característica de ser executada de forma independente e com eventos assíncronos com base em microserviços com o .NET Core e o Docker.

Os contêineres oferecem os benefícios de portabilidade, agilidade, escalabilidade, controle e isolamento em todo o fluxo de trabalho do ciclo de vida do aplicativo. 

![image](https://user-images.githubusercontent.com/19453244/129207906-9c06c7d5-3886-440e-8703-14122bb36550.png)

# Principais padrões utilizados no desenvolvimento da solução
[Aguarde]

# Principais tecnologias utilizadas
[Aguarde]

# Explorando a aplicação
1)	Camada de Apresentação: Web App MVC Core
URL: https://localhost:5006

![image](https://user-images.githubusercontent.com/19453244/129215456-9d120692-6008-4d1a-a730-6cefd9122bc9.png)

2) Autenticação no Identity Server - SSO
URL: http://localhost:5000
![image](https://user-images.githubusercontent.com/19453244/129215180-ac5106d6-0674-4017-8c60-7a9a669cc485.png)

As APIs utilizam as credenciais abaixo para a autorização via token JWT:

- Authority: "https://localhost:5001"
- ClientId: "715000d0c10040258c1be259c09e3b91"
- ClientSecret: "360ceac2e80545dca6083fef4f94d09f"
- Scopes: openid | profile | api_favo_mel

3) Swagger UI – API REST microserviço – Catálogo/Produto
URL: https://localhost:5101

![image](https://user-images.githubusercontent.com/19453244/129218403-fbe97d75-d50e-4627-8a0a-33554de83654.png)


4) Swagger UI – API REST microserviço – Venda
URL: https://localhost:5003

![image](https://user-images.githubusercontent.com/19453244/129219798-b693cc0e-b5d5-4d90-8c37-8d0da7ab3456.png)


5) Banco de Dados

Server: tcp:127.0.0.1,11433
User: sa
Password: Numsey#2021

![image](https://user-images.githubusercontent.com/19453244/129221734-2a1bc9b8-b48d-4251-8efd-b1b9ebcb676f.png)


Strings de conexão:

"VendaDbConnection": "Server=tcp:127.0.0.1,11433;Database=VendaDb;User Id=sa;Password=Numsey#2021"

"CatalogoDbConnection": "Server=tcp:127.0.0.1,11433;Database=CatalogoDb;User Id=sa;Password=Numsey#2021"
