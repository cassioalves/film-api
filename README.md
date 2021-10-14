# API para consulta o maior e o menor período de conquistas dos prêmios consecutivos

Funcionamento
 - Ao subir a aplicação a planilha (movielist.csv), contida no diretório Film.WebApi, será lida e seu conteúdo será armazenado no BD em memória;
 - Posteriormente é possível executar o método Rest exposto "GetProducers";
 - A ação é responsável por percorrer todos os registros e filtar os produtores que ganharam prêmios; Devolvendo o maior e o menor período consecutivo;
 - O resultado será um json contendo os dados consultados;
 - Quando um grupo de produtores ganhar o prêmio a aplicação irá considerar de forma individual (ex: dois produtores ganharam um prêmio em um determinado ano e no ano seguinte apenas um dos produtores ganhou um prêmio, ele será indicado como o menor período por ter ganho com diferença de um ano);

Logs
 - Cada importação gera um log na raíz da WebApi (Film.WebApi) com o nome no padrão (log_DATA_HORA.txt);
 - No arquivo de log é possível verificar se houve algum erro na importação e saber o motivo/linha do problema;
