using APIStreamingDeAudio.DTOs;
using APIStreamingDeAudio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIStreamingDeAudio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaixaAudioController : ControllerBase
{
    private readonly IFaixaAudioService _service;

    public FaixaAudioController(IFaixaAudioService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna todas as faixas de áudio cadastradas.
    /// </summary>
    /// <returns>Uma lista contendo todas as faixas de áudio.</returns>
    /// <remarks>
    /// Regra de Negócio: Retorna o catálogo completo. Não requer parâmetros.
    /// Exemplo de Resposta:
    /// [
    ///   {
    ///     "id": 1,
    ///     "titulo": "Bohemian Rhapsody",
    ///     "nomeArtista": "Queen",
    ///     "generoMusical": "Rock",
    ///     "duracaoEmSegundos": 354
    ///   }
    /// ]
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FaixaAudioResponseDTO>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var faixas = await _service.GetAllAsync();
        return Ok(faixas);
    }

    /// <summary>
    /// Retorna uma faixa de áudio específica pelo seu ID.
    /// </summary>
    /// <param name="id">O ID único da faixa de áudio.</param>
    /// <returns>A faixa de áudio correspondente ao ID.</returns>
    /// <remarks>
    /// Regra de Negócio: Caso o ID não exista na base de dados, retornará 404 Not Found.
    /// Exemplo de Resposta:
    /// {
    ///   "id": 1,
    ///   "titulo": "Bohemian Rhapsody",
    ///   "nomeArtista": "Queen"
    /// }
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FaixaAudioResponseDTO), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var faixa = await _service.GetByIdAsync(id);
        if (faixa == null) return NotFound("Faixa de áudio não encontrada.");
        return Ok(faixa);
    }

    /// <summary>
    /// Cadastra uma nova faixa de áudio na plataforma.
    /// </summary>
    /// <param name="requestDto">Objeto contendo os dados da nova faixa de áudio.</param>
    /// <returns>A faixa de áudio recém-criada.</returns>
    /// <remarks>
    /// Regra de Negócio: O sistema preencherá automaticamente os campos DataDeUpload (data atual) e TotalReproducoes (zero).
    /// Exemplo de Requisição (JSON):
    /// {
    ///   "titulo": "Fear of the Dark",
    ///   "nomeArtista": "Iron Maiden",
    ///   "nomeAlbum": "Fear of the Dark",
    ///   "generoMusical": "Heavy Metal",
    ///   "duracaoEmSegundos": 438,
    ///   "caminhoDoArquivo": "/storage/music/fear_of_the_dark.mp3",
    ///   "formatoArquivo": "mp3",
    ///   "taxaDeBits": 320,
    ///   "frequenciaAmostragem": 44100,
    ///   "tamanhoArquivoBytes": 12000000,
    ///   "dataDeLancamento": "1992-05-11T00:00:00Z",
    ///   "nomeCompositor": "Steve Harris",
    ///   "nomeGravadora": "EMI",
    ///   "contemConteudoExplicito": false
    /// }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(FaixaAudioResponseDTO), 201)]
    public async Task<IActionResult> Create([FromBody] FaixaAudioRequestDTO requestDto)
    {
        var createdFaixa = await _service.CreateAsync(requestDto);
        return CreatedAtAction(nameof(GetById), new { id = createdFaixa.Id }, createdFaixa);
    }

    /// <summary>
    /// Atualiza integralmente os dados de uma faixa de áudio existente.
    /// </summary>
    /// <param name="id">O ID da faixa de áudio a ser atualizada.</param>
    /// <param name="requestDto">Objeto contendo os novos dados da faixa.</param>
    /// <returns>Sem conteúdo em caso de sucesso (204 No Content).</returns>
    /// <remarks>
    /// Regra de Negócio: A atualização substitui todos os campos preenchíveis. Data de upload e Reproduções permanecem inalteradas.
    /// Exemplo de Requisição (JSON):
    /// {
    ///   "titulo": "Fear of the Dark - Remastered",
    ///   "nomeArtista": "Iron Maiden",
    ///   "formatoArquivo": "flac",
    ///   "generoMusical": "Heavy Metal"
    ///   // (incluir os demais campos obrigatórios do request)
    /// }
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] FaixaAudioRequestDTO requestDto)
    {
        var success = await _service.UpdateAsync(id, requestDto);
        if (!success) return NotFound("Faixa de áudio não encontrada para atualização.");
        return NoContent();
    }

    /// <summary>
    /// Remove permanentemente uma faixa de áudio da plataforma.
    /// </summary>
    /// <param name="id">O ID da faixa de áudio a ser deletada.</param>
    /// <returns>Sem conteúdo em caso de sucesso (204 No Content).</returns>
    /// <remarks>
    /// Regra de Negócio: A remoção é física no banco de dados (Hard Delete).
    /// </remarks>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return NotFound("Faixa de áudio não encontrada para exclusão.");
        return NoContent();
    }
}