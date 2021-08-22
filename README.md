## Visão Geral da Aplicação ‘Favo de Mel’
O objetivo geral desse projeto ‘Favo de Mel’ é o fornecer uma nova ferramenta que ajude o restaurante fictício ‘Favo de Mel’ a gerenciar o atendimento de seus clientes. A ferramenta foi desenvolvida como parte de avaliação do processo seletivo para a vaga de Arquiteto de Sistemas da empresa Ewave. 
Os principais recursos disponíveis são:
- Garçom: visualizar comandas abertas, abrir comanda, adicionar pedido a comanda, cancelar pedido da comanda, acompanhar o status de um pedido na cozinha e fechar a comanda;
- Cozinha: visualizar, receber e entregar o pedido pronto para o garçom.
- Notificação ativa entre o garçom e a cozinha ou vice-versa;
- Garçom poder visualizar o andamento de preparo dos pedidos de uma comanda.

O sistema terá em breve novas atualizações com os seguintes recursos:
- Versão de front-end com SPA em Angular;
- Cache distribuídos no microserviço de vendas (comandas) para melhorar o tempo de resposta da aplicação;
- Notificação de processos com SignalR nos principais eventos de comunicação entre atendimento (garçom) e cozinha;
- Orquestração de containers com Kubernetes.

Qualquer dúvida ou sugestão entre em contato através do e-mail marcio.queiroz@ewave.com.br 


## Visão Geral da Arquitetura

A ferramenta ‘Favo de Mel’ foi desenvolvida em arquitetura de microserviços para ser implantado em contêiners do Docker. O aplicativo é composto com front-end em ASP.NET MVC Core.
A justificativa da escolha da arquitetura de microserviços foi a de permitir a facilidade de manutenção das unidades de subsistemas de maneira independente, ou seja, cada microserviço tem o ciclo de vida autônomo e granular ao invés de um único aplicativo monolítico.  Principais características:

- Cada serviço é executado em seu próprio processo e comunica-se com outros processos usando protocolos HTTP/HTTPS e broker AMQP (RabbitMQ).
- A aplicação possui um microserviço de autenticação e autorização que fornece um token JWT com as informações do usuário, bem como as claims que restringem ou permitem acesso em determinada parte da aplicação. 
 

![image](https://user-images.githubusercontent.com/19453244/130339877-7137cd08-cb76-44da-94a1-9ed06fb88e51.png)

- O monitoramento e verificações de integridade dos microserviços e da infraestrutura são feitos pela biblioteca ASP.NET Health Checks.
- Os eventos de logs e diagnóstico são feitos para cada microserviço e disponibilizados em tempo real em ambientes elasticsearch + Kibana.
- Os registros de todos os eventos são persistidos em um banco NoSQL EventStore para a implementação do padrão arquitetural Event Sourcing.

### Principais padrões e tecnologias utilizadas no desenvolvimento da solução

- Domain Driven Design
- CQRS com MediatR
- TDD, BDD e Principios SOLID
- Padrões Unit of Work, Repository, Factory
- Docker e orquestração dom Docker-Compose
- ASP.NET Core MVC 5
- ASP.NET Health Checks
- WEB API Core - Swagger UI
- Entity Framework Core
- Banco Relaciona MSSQL Server
- Banco não relacional EventStore e ElasticSearch
- Dapper
- AutoMapper
- Inversão de Controle com .NET Core Native DI
- FluentValidation
- Padrão EventBus com a implementação RabbitMQ
- XUnit com testes de unidade e funcional com banco de dados em memória com EntityFramework Core
- EventSourcing
- Domain Events e Integration Events
- Autenticação e Autorização OAuth2 e JWT
- Carga de dados (Seed) em contâiner Docker com Migrations EntityFramework Core

### Explore o código da aplicação
- Acesse o <a href="https://github.com/MaqBr/ewave-favo-de-mel-arquiteto/wiki/Vis%C3%A3o-geral-do-c%C3%B3digo-da-aplica%C3%A7%C3%A3o">Wiki</a> para detalhes da arquitetura da aplicação.

## Como usar a aplicação
### 1. Pré-requisitos para utilização:

- Pré-requisitos para utilização:
  - Docker https://docs.docker.com/docker-for-windows/install/
  - .NET Core 5 https://dotnet.microsoft.com/download/dotnet/5.0 
  - Visual Studio Code ou qualquer outro IDE compatível com .NET

- Atenção: configure o Docker com (no mínimo) memória e CPU:
  - Memory: 6 GB
  - CPU: 2

![image](https://user-images.githubusercontent.com/19453244/130338740-3f2010b4-45de-4c9b-b49c-ad91e9342d10.png)

- Não é necessário configurar o Docker manualmente se estiver usando  Docker Desktop WSL 2 backend 

### 2 . Executar a aplicação com Docker Compose:

     - Clonar o código: 
        git clone https://github.com/MaqBr/ewave-favo-de-mel-arquiteto.git
        
     - No diretório raiz executar os comandos:
        docker-compose build
        docker-compose up -d
        
### 3 . Abrir a URL da API WebStatus
     - http://host.docker.internal:5160
 
Na primeira execução pode acontecer eventualmente uma falha de comunicação com os microserviços API Vendas 'Connection refused (catalogo-api:80)' e API Catalogo 'Connection refused (catalogo-api:80)' conforme ilustra a imagem abaixo.  Caso aconteça a falha, aguarde aproximadamente 10 segundos até que todos os recursos fiquem disponíveis (cor verde).

![image](https://user-images.githubusercontent.com/19453244/130352985-3028d8bb-6cde-4d38-87f6-5c084ce3a0a4.png)


![image](https://user-images.githubusercontent.com/19453244/130339052-671ec20b-7a20-4225-bb5f-382cf1f41dda.png)

### 4 . Abrir a URL da Aplicação MVC
     - http://host.docker.internal:5130

As informações de autenticação estão no rodapé da página:

![image](https://user-images.githubusercontent.com/19453244/130339185-0d56f131-235f-48f4-a58d-2e5ff2e31af8.png)

A aplicação possue 2 perfis de usuários:

 - Perfil Garçom
   - Usuário: garcom@teste.com
   - Senha: Teste@123
 
 - Perfil Cozinha
   - Usuário: cozinha@teste.com
   - Senha: Teste@123  

### Detalhes de uso da aplicação
- Acesse o <a href="https://github.com/MaqBr/ewave-favo-de-mel-arquiteto/wiki/O-uso-da-aplica%C3%A7%C3%A3o-ASP.NET-MVC-Core">Wiki</a> para detalhes do uso da aplicação.

### 5 . URL API de Autenticação
     - http://host.docker.internal:5000

Para exemplificar a geração de token JWT, utilize uma das contas disponíveis descrito no item 4 deste documento. 

A imagem a seguir ilustra o exemplo de uma requisição com o usuário garcom@teste.com.  Observe que a resposta veio com o token JWT e informações de claims do usuário.
Não foi utilizado protocolo HTTPS por se tratar de um ambiente de desenvolvimendo.
Todas as aplicações clientes recebem as credenciais através desta API.  

![image](https://user-images.githubusercontent.com/19453244/130338997-988622ec-5f40-4894-b872-dd7e2da59b9e.png)

### 6 . URL API de Venda

A API de venda foi desenvolvida para consumo apenas com clientes autenticados via OAuth2.  A autenticação foi retirada por se tratar de um ambiente de desenvolvimento e devido a problemas de instalção de certificados HTTPS em contâiners docker local.
Toda as funcionalidades de geranciamento de comandas, mesas e pedidos são geranciados por essa API.

![image](https://user-images.githubusercontent.com/19453244/130339241-2de6941a-f2c8-422a-854d-7946faa88859.png)

### 7 . URL API de Catálogo

A API de catálogo foi desenvolvida para consumo de clientes sem a autenticação por se tratar de apenas leitura.  
As informações de produtos, categorias, estoque e vouchers são gerenciados por essa API.

### 8 . URL RabbitMQ

     - http://host.docker.internal:15672
     - Username: guest
     - Password: guest
     - Exchange: favodemel_event_bus do tipo Direct durável
     - Queue: favoDeMelQueue
     
A comunicação entre os microserviços ocorrem através da implementação de um EventBus para o RabbitMQ.  

     
 ![image](https://user-images.githubusercontent.com/19453244/130339542-2cba73f4-cc8f-4e9c-a851-fdb53073a5d2.png)

### 9 . Banco de Dados SQL Server

     - host.docker.internal,5433
     - Username: sa
     - Password: P@ssw0rd
     - String de conexão para o banco do microserviço de autenticação: Server=host.docker.internal,5433;Database=IdentityDb;User Id=sa;Password=P@ssw0rd
     
![image](https://user-images.githubusercontent.com/19453244/130339603-c41ec05e-258f-4c5b-bbb6-c545fdbd35f5.png)
  
     - String de conexão para o banco de microserviço de catálogo: Server=host.docker.internal,5433;Database=CatalogoDb;User Id=sa;Password=P@ssw0rd
     
![image](https://user-images.githubusercontent.com/19453244/130339621-37215e24-65fa-45d3-a9ad-4ca3ff733d6e.png)
         
     - String de conexão para o banco de microserviço de venda: Server=host.docker.internal,5433;Database=VendaDb;User Id=sa;Password=P@ssw0rd
![image](https://user-images.githubusercontent.com/19453244/130339620-57320986-ba10-421e-9f16-f4f2d25f2240.png)

### 10 . URL de logs
     - http://host.docker.internal:5601 
     
Disponível as informações de todos os logs de erros com o ElasticSearch + Kibana.

### 11 . URL do EventStore
Esse recurso implementa o padrão Event Sourcing que utiliza o EventStore, um bando de banco de dados para armazenagem de eventos. Os eventos são armazenados e resgatados do EventStore disponível na URL:
     - http://host.docker.internal:2113

![image](https://user-images.githubusercontent.com/19453244/130351952-df029e19-9fa0-450c-bcac-31b9e271312a.png)


## Acesse o Wiki

Acesse o <a href=https://github.com/MaqBr/ewave-favo-de-mel-arquiteto/wiki/Seja-bem-vindo-ao-wiki-do-projeto-%E2%80%98Favo-de-Mel%E2%80%99>Wiki</a> para saber mais detalhes da arquitetura e uso geral da aplicação.
