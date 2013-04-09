SushiOrder

Esta é uma solução VS 2012 contendo 5 projetos. Contém uma aplicação web que chama um método remoto num objeto servido por um outro processo consola.

Common
======
Projeto contendo a definição da interface do objeto remoto e de um objeto seriazável para transporte de informação entre clientes e objeto remoto.

Orders
======
Objeto remoto contendo um método que retorna encomendas feitas de um utilizador (com uma entrada introduzida no construtor).

Server
======
Aplicação consola servidora do objeto remoto.

Client
======
Cliente consola que chama (e apresenta resultados) o método remoto disponibilizado pelo objeto remoto.

SushiOrder
==========
Projeto de um website ASP.NET (colocar no subdiretório WebSites do VS2012 (criar se não existir)). Este website referencia o objeto remoto, construindo um proxy (ver Web.config e código do site), e chamando o seu método remoto no handler do botão (quando de um postback). O resultado da chamada é apresentado num controlo de interface do tipo GridView, disponível em ASP.NET.
