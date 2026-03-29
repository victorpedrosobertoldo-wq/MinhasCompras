using MinhasCompras.Models;

namespace MinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    App PropriedadesApp;
    public NovoProduto()
    {
        InitializeComponent();
        
        PropriedadesApp = (App)Application.Current;
        picker_categoria.ItemsSource = PropriedadesApp.list_categoria;
    }

    private async void Salvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var categoriaSelecionada = picker_categoria.SelectedItem as Categorias;

            if (categoriaSelecionada == null)
            {
                await DisplayAlert("Erro", "Selecione uma categoria!", "OK");
                return;
            }

            Produto p = new Produto
            {
                Descricao = DescricaoEntry.Text,
                Quantidade = Convert.ToDouble(QuantidadeEntry.Text),
                Preco = Convert.ToDouble(PrecoUnitarioEntry.Text),
                Categoria = categoriaSelecionada.categoria_selecionada
            };


            await App.Db.insert(p);
            await DisplayAlertAsync("Sucesso", "Produto Incerido", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }





}
