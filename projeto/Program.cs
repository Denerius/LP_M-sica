using biblioteca.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.WebHost.UseUrls("http://0.0.0.0:8000");

var app = builder.Build();

app.UseCors("AllowAll");

Musica[] listaMusica = new Musica[100];
int totalMusica = 0;

app.MapGet("/", () =>
{
    return Results.Ok("API Biblioteca de Música funcionando com sucesso!");
});

app.MapPost("/musica", (JsonElement body) =>
{     
    Random random = new();

    Musica musica = new Musica ();

    bool idExiste = true;
    while (idExiste)
    {
        musica.Id = random.Next(1000, 9999);
        idExiste = false;

        for (int i = 0; i < totalMusica; i++)
        {
            if (listaMusica[i].Id == musica.Id)
            {
                idExiste = true;
                break;
            }
        }
    }

    musica.Titulo = body.GetProperty("titulo").GetString();
    musica.Artista = body.GetProperty("artista").GetString();
    musica.Compositor = body.GetProperty("compositor").GetString();
    musica.Genero = body.GetProperty("genero").GetString();
    musica.Ano = body.GetProperty("ano").GetInt32();


    listaMusica[totalMusica] = musica;
    totalMusica++;

    return Results.Ok( new {
    musica
        }
    );
});

app.MapGet("/musica", () =>
{
    Musica[] musicaCadastrados = new Musica[totalMusica];

    for (int i = 0; i < totalMusica; i++)
    {
        musicaCadastrados[i] = listaMusica[i];
    }

    return Results.Ok(new
    {
        musicaCadastrados
    });
});

app.MapPatch("/musica/{id}", (int id, JsonElement body) =>
{
    string novo_titulo = body.GetProperty("salario").GetString();

    for (int i = 0; i < totalMusica; i++)
    {
        if (listaMusica[i].Id == id)
        {
            listaMusica[i].Titulo = novo_titulo;
            return Results.Ok(
                new
                {
                    musica = listaMusica[i]
                }
            );
        }
    }

    return Results.NotFound(new
    {
        message = "Música não encontrada."
    });
});


app.MapDelete("/musica/delete/{id}", (int id) =>
{
    for (int i = 0; i < totalMusica; i++)
    {
        if (listaMusica[i].Id == id)
        {
            Musica musicaRemovida = listaMusica[i];
            
            for (int j = i; j < totalMusica - 1; j++)
            {
                listaMusica[j] = listaMusica[j + 1];
            }            

            totalMusica--;

            return Results.Ok(new
            {
                mensagem = "Música removida com sucesso.",
                musica = musicaRemovida
            });
        }
    }

    return Results.NotFound(new
    {
        message = "Música não encontrada."
    });
});

app.MapGet("/musica/artista/busca/{artista}", (string artista) =>
{
    Musica[] musicaEncontrados = new Musica[totalMusica];

    int totalEncontrados = 0;

    for (int i = 0; i < totalMusica; i++)
    {
        if (listaMusica[i].Artista.ToLower() == artista.ToLower())
        
        {
            musicaEncontrados[totalEncontrados] = listaMusica[i];
            totalEncontrados++;
        }
    }

    if (totalEncontrados > 0)
    {
        Musica[] resultadoFinal = new Musica[totalEncontrados];

        for (int i = 0; i < totalEncontrados; i++)
        {
            resultadoFinal[i] = musicaEncontrados[i];
        }        

        return Results.Ok(new
        {
            artista,
            musica = musicaEncontrados
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada para esse artista."
    });
});

app.MapGet("/musica/busca/{titulo}", (string titulo) =>
{
    Musica[] musicaEncontrados = new Musica[totalMusica];

    int totalEncontrados = 0;

    for (int i = 0; i < totalMusica; i++)
    {
        if (listaMusica[i].Titulo.ToLower() == titulo.ToLower())
        {
            musicaEncontrados[totalEncontrados] = listaMusica[i];
            totalEncontrados++;
        }
    }

    if (totalEncontrados > 0)
    {
        Musica[] resultadoFinal = new Musica[totalEncontrados];

        for (int i = 0; i < totalEncontrados; i++)
        {
            resultadoFinal[i] = musicaEncontrados[i];
        }        

        return Results.Ok(new
        {
            titulo,
            musica = musicaEncontrados
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada com esse título."
    });
});

app.MapGet("/musica/busca/{ano}", (int ano) =>
{
    Musica[] musicaEncontrados = new Musica[totalMusica];

    int totalEncontrados = 0;

    for (int i = 0; i < totalMusica; i++)
    {
        if (listaMusica[i].Ano == ano)
        {
            musicaEncontrados[totalEncontrados] = listaMusica[i];
            totalEncontrados++;
        }
    }

    if (totalEncontrados > 0)
    {
        Musica[] resultadoFinal = new Musica[totalEncontrados];

        for (int i = 0; i < totalEncontrados; i++)
        {
            resultadoFinal[i] = musicaEncontrados[i];
        }        

        return Results.Ok(new
        {
            ano,
            musica = musicaEncontrados
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada com esse ano."
    });
});

app.MapGet("/musica/busca/{compositor}", (string compositor) =>
{
    Musica[] musicaEncontrados = new Musica[totalMusica];

    int totalEncontrados = 0;

    for (int i = 0; i < totalMusica; i++)
    {
        if (listaMusica[i].Compositor.ToLower() == compositor.ToLower())
        {
            musicaEncontrados[totalEncontrados] = listaMusica[i];
            totalEncontrados++;
        }
    }

    if (totalEncontrados > 0)
    {
        Musica[] resultadoFinal = new Musica[totalEncontrados];

        for (int i = 0; i < totalEncontrados; i++)
        {
            resultadoFinal[i] = musicaEncontrados[i];
        }        

        return Results.Ok(new
        {
            compositor,
            musica = musicaEncontrados
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada com esse compositor."
    });
});

app.MapGet("/musica/busca/{genero}", (string genero) =>
{
    Musica[] musicaEncontrados = new Musica[totalMusica];

    int totalEncontrados = 0;

    for (int i = 0; i < totalMusica; i++)
    {
        if (listaMusica[i].Genero.ToLower() == genero.ToLower())
        {
            musicaEncontrados[totalEncontrados] = listaMusica[i];
            totalEncontrados++;
        }
    }

    if (totalEncontrados > 0)
    {
        Musica[] resultadoFinal = new Musica[totalEncontrados];

        for (int i = 0; i < totalEncontrados; i++)
        {
            resultadoFinal[i] = musicaEncontrados[i];
        }        

        return Results.Ok(new
        {
            genero,
            musica = musicaEncontrados
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada para esse gênero."
    });
});

app.Run();