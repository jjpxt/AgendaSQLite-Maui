using MeuChat.Classes;
using System.Threading.Tasks;

namespace MeuChat.Pagina;

public partial class Contato : ContentPage
{
	public Contato(Pessoas pessoas)
	{
		InitializeComponent();
        BindingContext = pessoas;
	}



    private async void BTNSalvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            Pessoas contatoAtualizado = new()
            {
                Id = Convert.ToInt32(TXTID.Text),
                Nome = TXTNome.Text,
                Email = TXTEmail.Text,
                Telefone = TXTTel.Text,
                Anotacoes = TXTObs.Text
            };

            await App.BancoDeDados.SalvarPessoa(contatoAtualizado);
            await DisplayAlert("Salvo", "Atualizado com sucesso", "Ok");
            await Navigation.PopAsync();
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    private void BTNCancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}