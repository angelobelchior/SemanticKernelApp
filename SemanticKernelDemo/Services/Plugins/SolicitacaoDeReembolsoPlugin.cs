using System.ComponentModel;
using ChromaDB.Client;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;

namespace SemanticKernelDemo.Services.Plugins;

public class SolicitacaoDeReembolsoPlugin
{
    [KernelFunction]
    [Description("Instruções para solicitar o reembolso de despesas")]
    [return: Description("Lista de instruções")]
    public async Task<IEnumerable<Instrucoes>> SolicitacaoDeReembolso()
    {
        var configOptions = new ChromaConfigurationOptions(uri: "http://localhost:8000/api/v1/");
        using var httpClient = new HttpClient();
        var chromaClient = new ChromaClient(configOptions, httpClient);
        var collection = await chromaClient.GetOrCreateCollection("reembolsos");
        var collectionClient = new ChromaCollectionClient(collection, configOptions, httpClient);

        IEmbeddingGenerator<string, Embedding<float>> generator =
            new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "all-minilm");

        var input = "Solicitar Reembolso";
        var queryEmbedding = await generator.GenerateEmbeddingVectorAsync(input);
        var queryResult = await collectionClient.Query(
            queryEmbeddings: queryEmbedding,
            nResults: 20,
            include: ChromaQueryInclude.Metadatas | ChromaQueryInclude.Documents | ChromaQueryInclude.Distances);

        var instrucoes = new List<Instrucoes>();
        foreach (var result in queryResult)
        {
            instrucoes.Add(new(
                Titulo: result.Metadata?["Title"].ToString() ?? string.Empty,
                Descricao: result.Metadata?["Description"].ToString() ?? string.Empty
            ));
        }

        return instrucoes;
    }
}

public record Instrucoes(string Titulo, string Descricao);