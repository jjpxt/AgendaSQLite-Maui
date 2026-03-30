using SQLite;

namespace MeuChat.Classes;

public class Servicos
{
    private readonly SQLiteAsyncConnection _bancodedados;

    public Servicos()
    {
        var localBD = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "contatos.db3");
            _bancodedados = new SQLiteAsyncConnection(localBD);
            _bancodedados.CreateTableAsync<Pessoas>().Wait();   
    }

    public Task<int> SalvarPessoa(Pessoas pessoas)
    {
        if(pessoas.Id != 0)
        {
            return _bancodedados.UpdateAsync(pessoas);
        }
        else
        {
            return _bancodedados.InsertAsync(pessoas);
        }
    }

    public Task<List<Pessoas>> ListarPessoas()
    {
        return _bancodedados.Table<Pessoas>().ToListAsync();
    }
}
