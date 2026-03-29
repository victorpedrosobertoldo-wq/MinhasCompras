using MinhasCompras.Models;

namespace MinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = DescricaoEntry.Text,
                Quantidade = Convert.ToDouble(QuantidadeEntry.Text),
                Preco = Convert.ToDouble(PrecoUnitarioEntry.Text),
                Categoria = picker_categoria.SelectedItem.ToString()
            };

            await App.Db.update(p);
            await DisplayAlertAsync("Sucesso", "Produto Atualizado", "OK");
            await Navigation.PopAsync();


        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }
}