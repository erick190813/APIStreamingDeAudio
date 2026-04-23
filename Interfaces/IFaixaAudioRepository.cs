using APIStreamingDeAudio.Models;

namespace APIStreamingDeAudio.Interfaces;

public interface IFaixaAudioRepository
{
    Task<IEnumerable<FaixaAudio>> GetAllAsync();
    Task<FaixaAudio?> GetByIdAsync(int id);
    Task<FaixaAudio> AddAsync(FaixaAudio faixaAudio);
    Task UpdateAsync(FaixaAudio faixaAudio);
    Task DeleteAsync(FaixaAudio faixaAudio);
}