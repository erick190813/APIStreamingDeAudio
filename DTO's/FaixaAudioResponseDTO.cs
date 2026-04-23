namespace APIStreamingDeAudio.DTOs;

public class FaixaAudioResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string NomeArtista { get; set; } = string.Empty;
    public string NomeAlbum { get; set; } = string.Empty;
    public string GeneroMusical { get; set; } = string.Empty;
    public int DuracaoEmSegundos { get; set; }
    public string CaminhoDoArquivo { get; set; } = string.Empty;
    public string FormatoArquivo { get; set; } = string.Empty;
    public int TaxaDeBits { get; set; }
    public int FrequenciaAmostragem { get; set; }
    public long TamanhoArquivoBytes { get; set; }
    public DateTime DataDeLancamento { get; set; }
    public string NomeCompositor { get; set; } = string.Empty;
    public string NomeGravadora { get; set; } = string.Empty;
    public long TotalReproducoes { get; set; }
    public bool ContemConteudoExplicito { get; set; }
    public DateTime DataDeUpload { get; set; }
}