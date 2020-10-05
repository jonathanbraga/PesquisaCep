O projeto PesquisaCep foi todo desenvolvido em xamarin forms usando o padrão MVVM, para auxiliar nessa padrão 
foi utilizado o PRISM. O Projeto é dividido em 3 Layers principais .MOBILE, .SERVICE. MODEL. 
Onde o .MOBILE é responsável pela camada de aplicação e consumo dos serviços. A camada .SERVICE é responsável por fazer as chamadas ao serviços de consultar o cep
e por gerenciar algumas interfaces. A camada .MODEL é referente ao modelo de dados que se existe na aplicação ou algum DTO que por ventura venha a senhor necessário.

O Banco de dados local que foi utilizado foi o SQlLite, foi configurado em cada projeto .ANDROID e .IOS o caminho referente a cada plataforma.
Dentro dessa mesma base de dados foi criar uma única tabela para poder auxiliar no projeto.

Referente ao mapa, foi se usado a biblioteca xamarin.forms.maps.