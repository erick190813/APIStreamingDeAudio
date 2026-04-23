using APIStreamingDeAudio.Data;
using APIStreamingDeAudio.Interfaces;
using APIStreamingDeAudio.Models;
using Microsoft.EntityFrameworkCore;

namespace APIStreamingDeAudio.Repositories;

public class FaixaAudioRepository : IFaixaAudioRepository
{
    private readonly AppDbContext _context;

    public FaixaAudioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FaixaAudio>> GetAllAsync()
    {
        return await _context.FaixasAudio.ToListAsync();
    }

    public async Task<FaixaAudio?> GetByIdAsync(int id)
    {
        return await _context.FaixasAudio.FindAsync(id);
    }

    public async Task<FaixaAudio> AddAsync(FaixaAudio faixaAudio)
    {
        _context.FaixasAudio.Add(faixaAudio);
        await _context.SaveChangesAsync();
        return faixaAudio;
    }

    public async Task UpdateAsync(FaixaAudio faixaAudio)
    {
        _context.FaixasAudio.Update(faixaAudio);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(FaixaAudio faixaAudio)
    {
        _context.FaixasAudio.Remove(faixaAudio);
        await _context.SaveChangesAsync();
    }
}