# APIStreamingDeAudio

# 🎵 API Streaming de Áudio

## 📖 Visão Geral
A **API Streaming de Áudio** é uma aplicação backend RESTful desenvolvida em C# e .NET 8. O sistema tem como objetivo gerenciar um catálogo de músicas para uma plataforma de streaming, permitindo o cadastro, consulta, atualização e exclusão (CRUD completo) de faixas de áudio. 

O projeto foi construído seguindo rigorosamente os princípios de **Clean Code**, **Arquitetura em Camadas** (Separation of Concerns) e **Injeção de Dependência**, garantindo um código manutenível, escalável e seguro.

---

## 🛠️ Tecnologias e Ferramentas
* **Plataforma:** .NET 8.0 SDK
* **Linguagem:** C# 12
* **Banco de Dados:** SQLite (via `Microsoft.EntityFrameworkCore.Sqlite`)
* **ORM:** Entity Framework Core (`Microsoft.EntityFrameworkCore.Design`)
* **Documentação:** Swagger / OpenAPI (com suporte a XML Comments)

---

## 🏗️ Arquitetura do Projeto

A aplicação está dividida em diretórios lógicos para isolar responsabilidades:

* **`/Controllers`**: A porta de entrada da API. Recebe as requisições HTTP, valida as chamadas, delega o processamento para os *Services* e retorna os *Status Codes* apropriados (200, 201, 204, 404).
* **`/Services`**: O coração do sistema. Contém todas as regras de negócio. Validações, conversão de DTOs para Entidades e lógicas de inicialização (ex: setar data de upload) ocorrem aqui.
* **`/Repositories`**: Camada de Acesso a Dados. Isola o Entity Framework Core do resto do sistema. Responsável por executar as operações de *Insert, Update, Delete* e *Select* no SQLite.
* **`/Models`**: Contém as Entidades do domínio que mapeiam diretamente para as tabelas do banco de dados (ex: `FaixaAudio.cs`).
* **`/DTOs` (Data Transfer Objects)**: Objetos desenhados exclusivamente para o tráfego de dados na rede. Garantem que as Entidades do banco nunca sejam expostas diretamente ao cliente, aumentando a segurança e reduzindo *Over-Posting*.
* **`/Interfaces`**: Define os contratos (abstrações) para os *Services* e *Repositories*, permitindo a aplicação fluida da Injeção de Dependência no `Program.cs`.
* **`/Data`**: Configuração do `AppDbContext`, que representa a sessão com o banco de dados e gerencia o rastreamento de entidades.

---

## 🛡️ Segurança e Boas Práticas Implementadas

1. **Global Exception Handler:** A API possui um interceptador global de erros (`app.UseExceptionHandler`). Em caso de falha crítica (Erro 500), o sistema **nunca vaza a Stack Trace** ou dados internos do servidor, retornando apenas um JSON padronizado e amigável, prevenindo ataques de *Information Disclosure*.
2. **Tipagem Forte sem Enums:** Para garantir máxima flexibilidade na evolução do sistema (como adicionar novos gêneros ou formatos de arquivo no futuro sem recompilar a API), atributos como `GeneroMusical` e `FormatoArquivo` utilizam `String` estrita.
3. **Isolamento via DTOs:** Usuários não podem manipular métricas de sistema (como `TotalReproducoes` ou `DataDeUpload`) ao criar ou editar uma música, pois o `FaixaAudioRequestDTO` omite propositalmente esses campos, deixando a responsabilidade para o back-end.

---

## 💾 Dicionário de Dados (Entidade `FaixaAudio`)

O banco de dados SQLite armazena a entidade principal com 17 atributos:

| Atributo | Tipo C# | Tipo SQLite | Descrição |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `INTEGER` | Identificador único (Chave Primária, Autoincremento). |
| `Titulo` | `string` | `TEXT` | Nome da faixa musical. |
| `NomeArtista` | `string` | `TEXT` | Nome do cantor, banda ou grupo. |
| `NomeAlbum` | `string` | `TEXT` | Álbum correspondente à faixa. |
| `GeneroMusical` | `string` | `TEXT` | Categoria musical (ex: Rock, Pop, Clássica). |
| `DuracaoEmSegundos` | `int` | `INTEGER` | Tempo total de execução do áudio. |
| `CaminhoDoArquivo` | `string` | `TEXT` | Rota no storage/CDN (ex: `/musicas/audio.mp3`). |
| `FormatoArquivo` | `string` | `TEXT` | Extensão da mídia (ex: mp3, flac, wav). |
| `TaxaDeBits` | `int` | `INTEGER` | Qualidade do áudio (ex: 128, 256, 320 kbps). |
| `FrequenciaAmostragem`| `int` | `INTEGER` | Sample Rate em Hz (ex: 44100, 48000). |
| `TamanhoArquivoBytes` | `long` | `INTEGER` | Peso real do arquivo em disco. |
| `DataDeLancamento` | `DateTime` | `TEXT` | Data oficial do lançamento comercial. |
| `NomeCompositor` | `string` | `TEXT` | Nome do autor/escritor da música. |
| `NomeGravadora` | `string` | `TEXT` | Selo ou estúdio proprietário dos direitos. |
| `TotalReproducoes` | `long` | `INTEGER` | Quantidade de plays. Inicializado sempre como `0`. |
| `ContemConteudoExplicito`| `bool`| `INTEGER` | Flag de Parental Advisory (palavrões/temas adultos). |
| `DataDeUpload` | `DateTime` | `TEXT` | Carimbo de tempo de quando a faixa entrou no sistema. |

---

## 🚀 Como Executar o Projeto Localmente

### 1. Pré-requisitos
* Instalar o [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
* Instalar as ferramentas do Entity Framework Core globalmente (se ainda não tiver):
  ```bash
  dotnet tool install --global dotnet-ef
