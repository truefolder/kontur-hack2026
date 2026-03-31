using kontur_hack2026.Models;
using kontur_hack2026.Services;
using Microsoft.AspNetCore.Mvc;

namespace kontur_hack2026.Data;

public class InMemoryGeneratorRepository(IGeneratorService generatorService) : IGeneratorRepository
{
    private Dictionary<int, Generator> generators = new();
    private int _idCnt = 0;

    public int Add(JsonSchemaNode node)
    {
        _idCnt++;
        generators.Add(_idCnt, new Generator(generatorService,node));
        return _idCnt;
    }

    public Generator Get(int id)
    {
        return generators[id];
    }
}