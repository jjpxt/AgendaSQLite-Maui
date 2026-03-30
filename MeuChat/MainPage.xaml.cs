using MeuChat.Classes;
using MeuChat.Pagina;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MeuChat;

public partial class MainPage : ContentPage
{
    private bool deslizar = false;
    private ObservableCollection<Pessoas> pessoas;
    public MainPage()
    {
        InitializeComponent();
        pessoas = new ObservableCollection<Pessoas>();

        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregaLista();
    }

    public async Task CarregaLista()
    {
        var listaPessoas = await App.BancoDeDados.ListarPessoas();
        pessoas.Clear();

        foreach (var pessoa in listaPessoas)
        {
            pessoas.Add(pessoa);
        }

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

            pessoas.Add(pessoa);
        }
        catch(Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    private void CVLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (deslizar) return;

        var contato = e.CurrentSelection.FirstOrDefault() as Pessoas;
        Navigation.PushAsync(new Contato(contato));
    }

    private async void SwipeItem_Invoked(object sender, EventArgs e)
    {
        var item = ((SwipeItem)sender).BindingContext as Pessoas;
        var resposta = await DisplayAlert("Apagar", "Deseja apagar o contato?", "Sim", "Não");
        if (resposta)
        {
            await App.BancoDeDados.ApagarPessoa(item);
            await DisplayAlert("Apagado", "Contato apagado com sucesso", "Ok");

            pessoas.Remove(item);
        }
        else
        {
            await DisplayAlert("Cancelado", "Operação cancelada", "Ok");
        }
    }

    private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        deslizar = true;
    }

    private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        deslizar = false;
    }
}
