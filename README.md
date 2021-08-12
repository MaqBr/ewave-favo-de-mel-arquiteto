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

# Visão Geral da Arquitetura

Esta aplicação é cross-pataform e apresenta uma proposta inicial da solução ‘Favo de Mel’ no qual tem a principal característica de ser executada de forma independente e com eventos assíncronos com base em microserviços com o .NET Core e o Docker.

Os contêineres oferecem os benefícios de portabilidade, agilidade, escalabilidade, controle e isolamento em todo o fluxo de trabalho do ciclo de vida do aplicativo. 

![image](https://user-images.githubusercontent.com/19453244/129207906-9c06c7d5-3886-440e-8703-14122bb36550.png)



