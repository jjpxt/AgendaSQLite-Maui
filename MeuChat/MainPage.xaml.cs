using MeuChat.Classes;
using MeuChat.Pagina;

namespace MeuChat;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
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

    private void CVLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var contato = e.CurrentSelection.FirstOrDefault() as Pessoas;
        Navigation.PushAsync(new Contato(contato));
    }
}
