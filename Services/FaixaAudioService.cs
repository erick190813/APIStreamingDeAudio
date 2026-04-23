using APIStreamingDeAudio.DTOs;
using APIStreamingDeAudio.Interfaces;
using APIStreamingDeAudio.Models;

namespace APIStreamingDeAudio.Services;

public class FaixaAudioService : IFaixaAudioService
{
    private readonly IFaixaAudioRepository _repository;

    public FaixaAudioService(IFaixaAudioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FaixaAudioResponseDTO>> GetAllAsync()
    {
        var faixas = await _repository.GetAllAsync();
        return faixas.Select(MapToResponseDTO);
    }

    public async Task<FaixaAudioResponseDTO?> GetByIdAsync(int id)
    {
        var faixa = await _repository.GetByIdAsync(id);
        return faixa == null ? null : MapToResponseDTO(faixa);
    }

    public async Task<FaixaAudioResponseDTO> CreateAsync(FaixaAudioRequestDTO requestDto)
    {
        var faixa = MapToEntity(requestDto);
        faixa.DataDeUpload = DateTime.UtcNow;
        faixa.TotalReproducoes = 0; // Inicializa com 0

        var createdFaixa = await _repository.AddAsync(faixa);
        return MapToResponseDTO(createdFaixa);
    }

    public async Task<bool> UpdateAsync(int id, FaixaAudioRequestDTO requestDto)
    {
        var existingFaixa = await _repository.GetByIdAsync(id);
        if (existingFaixa == null) return false;

        existingFaixa.Titulo = requestDto.Titulo;
        existingFaixa.NomeArtista = requestDto.NomeArtista;
        existingFaixa.NomeAlbum = requestDto.NomeAlbum;
        existingFaixa.GeneroMusical = requestDto.GeneroMusical;
        existingFaixa.DuracaoEmSegundos = requestDto.DuracaoEmSegundos;
        existingFaixa.CaminhoDoArquivo = requestDto.CaminhoDoArquivo;
        existingFaixa.FormatoArquivo = requestDto.FormatoArquivo;
        existingFaixa.TaxaDeBits = requestDto.TaxaDeBits;
        existingFaixa.FrequenciaAmostragem = requestDto.FrequenciaAmostragem;
        existingFaixa.TamanhoArquivoBytes = requestDto.TamanhoArquivoBytes;
        existingFaixa.DataDeLancamento = requestDto.DataDeLancamento;
        existingFaixa.NomeCompositor = requestDto.NomeCompositor;
        existingFaixa.NomeGravadora = requestDto.NomeGravadora;
        existingFaixa.ContemConteudoExplicito = requestDto.ContemConteudoExplicito;

        await _repository.UpdateAsync(existingFaixa);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingFaixa = await _repository.GetByIdAsync(id);
        if (existingFaixa == null) return false;

        await _repository.DeleteAsync(existingFaixa);
        return true;
    }

    // Métodos de mapeamento manual para evitar dependência extra (AutoMapper) em projeto limpo
    private static FaixaAudioResponseDTO MapToResponseDTO(FaixaAudio faixa) => new()
    {
        Id = faixa.Id,
        Titulo = faixa.Titulo,
        NomeArtista = faixa.NomeArtista,
        NomeAlbum = faixa.NomeAlbum,
        GeneroMusical = faixa.GeneroMusical,
        DuracaoEmSegundos = faixa.DuracaoEmSegundos,
        CaminhoDoArquivo = faixa.CaminhoDoArquivo,
        FormatoArquivo = faixa.FormatoArquivo,
        TaxaDeBits = faixa.TaxaDeBits,
        FrequenciaAmostragem = faixa.FrequenciaAmostragem,
        TamanhoArquivoBytes = faixa.TamanhoArquivoBytes,
        DataDeLancamento = faixa.DataDeLancamento,
        NomeCompositor = faixa.NomeCompositor,
        NomeGravadora = faixa.NomeGravadora,
        TotalReproducoes = faixa.TotalReproducoes,
        ContemConteudoExplicito = faixa.ContemConteudoExplicito,
        DataDeUpload = faixa.DataDeUpload
    };

    private static FaixaAudio MapToEntity(FaixaAudioRequestDTO dto) => new()
    {
        Titulo = dto.Titulo,
        NomeArtista = dto.NomeArtista,
        NomeAlbum = dto.NomeAlbum,
        GeneroMusical = dto.GeneroMusical,
        DuracaoEmSegundos = dto.DuracaoEmSegundos,
        CaminhoDoArquivo = dto.CaminhoDoArquivo,
        FormatoArquivo = dto.FormatoArquivo,
        TaxaDeBits = dto.TaxaDeBits,
        FrequenciaAmostragem = dto.FrequenciaAmostragem,
        TamanhoArquivoBytes = dto.TamanhoArquivoBytes,
        DataDeLancamento = dto.DataDeLancamento,
        NomeCompositor = dto.NomeCompositor,
        NomeGravadora = dto.NomeGravadora,
        ContemConteudoExplicito = dto.ContemConteudoExplicito
    };
}