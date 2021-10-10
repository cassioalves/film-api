# API para consulta o maior e o menor período de conquistas dos prêmios consecutivos

Obs: fiquei em dúvida se deveria considerar conquista individual ou por registro. Na aplicação está sendo considerado conquista por registro da planilha CSV.

Funcionamento
 - Ao subir a aplicação a planilha (movielist.csv), contida no diretório Film.WebApi, será lida e seu conteúdo será armazenado no BD em memória;
 - Posteriormente é possível executar o método Rest exposto "GetProducers";
 - A ação é responsável por percorrer todos os registros e filtar os produtores que ganharam prêmios; Devolvendo o maior e o menor período consecutivo;
 - O resultado será um json contendo os dados consultados;
