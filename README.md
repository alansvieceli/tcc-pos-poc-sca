# Sistema de Controle Ambiental (SCA)
POC para o TCC da Pós em Arquitetura de Sistemas Distribuidos / Puc Minas

![alt text](https://raw.githubusercontent.com/alansvieceli/tcc-pos-poc-sca/master/diagrama.png)

## Preparar Banco de dados (dentro da pasta do projeto)

```
$ docker-compose -f docker-composeMySQL.yxml up -d

$ cd \SCA.Service.Auth && dotnet ef database update && cd ..\SCA.Service.Inputs && dotnet ef database update && cd ..\SCA.Service.Maintenance && dotnet ef database update && cd ..\SCA.Service.Monitoring && dotnet ef database update

$ cd .. && docker-compose down
```

## Inicia Sistema

```
$ docker-compose up -d --build
```

# Links Úteis


- http://localhost:81/  (principal do sistema)

- http://localhost:7000/auth/swagger (módulo de autenticação)
- http://localhost:7000/input/swagger (módulo de insumos / CRUD)
- http://localhost:7000/maintenance/swagger (módulo de manutenção)
- http://localhost:7000/monitoring/swagger (módulo de monitoramento)
- http://localhost:7000/alert/swagger (módulo de alerta, envio de mensagens)

Obs: add autencicação "Bearer <token>"


# Acessar sistema

- http://localhost:81/

