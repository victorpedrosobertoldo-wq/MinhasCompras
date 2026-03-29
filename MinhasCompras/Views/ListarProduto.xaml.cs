using MinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MinhasCompras.Views;

public partial class ListarProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();



    public ListarProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;

        var app = (App)Application.Current;

        picker_filtro.ItemsSource = app.list_categoria;
        picker_filtro.ItemDisplayBinding = new Binding("categoria_selecionada");
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }



    private void Adicionar_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }
    private void Somar_Clicked(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"o Total é {soma:c}";

        DisplayAlertAsync("Total Dos Produtos", msg, "ok");
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lista.Clear();

            lst_produtos.IsRefreshing = true;

            List<Produto> tmp = await App.Db.search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;
            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlertAsync("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });

        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }

    }

    private async void picker_filtro_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var categoriaSelecionada = picker_filtro.SelectedItem as Categorias;

            lista.Clear();

            List<Produto> todos = await App.Db.GetAll();

            if (categoriaSelecionada == null)
            {

                todos.ForEach(i => lista.Add(i));
            }
            else
            {
                var filtrados = todos
                    .Where(p => p.Categoria == categoriaSelecionada.categoria_selecionada)
                    .ToList();

                filtrados.ForEach(i => lista.Add(i));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
    private async void Relatorio_Clicked(object sender, EventArgs e)
    {
        try
        {
            List<Produto> produtos = await App.Db.GetAll();

            var relatorio = produtos
                .GroupBy(p => p.Categoria)
                .Select(g => new
                {
                    Categoria = g.Key,
                    Total = g.Sum(p => p.Total)
                })
                .ToList();

            string msg = "";

            foreach (var item in relatorio)
            {
                msg += $"{item.Categoria}: {item.Total:C}\n";
            }

            await DisplayAlert("Total por Categoria", msg, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }



}