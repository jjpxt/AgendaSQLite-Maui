using MeuChat.Classes;
using System.Threading.Tasks;

namespace MeuChat;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        CarregaLista();
    }

    public void CarregaLista()
    {
        var pessoas = App.BancoDeDados.ListarPessoas().Result;
        CVLista.ItemsSource = pessoas;
    }

    private async void OnCouterClicked(object sender, EventArgs e)
    {
        try
        {
            var pessoa = new Pessoas
            {
                Nome = await DisplayPromptAsync("Nome","Digite o nome: ","Ok" ),
                Email = await DisplayPromptAsync("E-Mail", "Digite o E-Mail: ", "Ok"),
                Telefone = await DisplayPromptAsync("Nome", "Digite o telefone: ", "Ok"),
                Anotacoes = await DisplayPromptAsync("Nome", "Digite as anotações : ", "Ok"),
            };

            await App.BancoDeDados.SalvarPessoa(pessoa);
            await DisplayAlert("Sucesso", "Salvo com sucesso", "Ok");

            CarregaLista();
        }
        catch(Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

}
