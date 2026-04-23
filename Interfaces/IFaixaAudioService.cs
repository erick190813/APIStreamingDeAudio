using APIStreamingDeAudio.DTOs;

namespace APIStreamingDeAudio.Interfaces;

public interface IFaixaAudioService
{
    Task<IEnumerable<FaixaAudioResponseDTO>> GetAllAsync();
    Task<FaixaAudioResponseDTO?> GetByIdAsync(int id);
    Task<FaixaAudioResponseDTO> CreateAsync(FaixaAudioRequestDTO requestDto);
    Task<bool> UpdateAsync(int id, FaixaAudioRequestDTO requestDto);
    Task<bool> DeleteAsync(int id);
}