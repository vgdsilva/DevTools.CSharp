using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTools.CodeGenerator.Interactive
{
    public class EntityEditorConsole
    {
        private class EntityProperty
        {
            public string Nome { get; set; } = string.Empty;
            public string Tipo { get; set; } = "string";
            public string Atributos { get; set; } = string.Empty;
        }

        private static List<EntityProperty> properties = new();
        private static int selectedIndex = 0;

        public static void Iniciar(string nomeEntidade)
        {
            properties = new List<EntityProperty>();
            selectedIndex = 0;

            bool editando = true;
            ConsoleKeyInfo key;

            while ( editando )
            {
                Console.Clear();
                ExibirCabecalho(nomeEntidade);
                ExibirPropriedades();

                key = Console.ReadKey(true);

                switch ( key.Key )
                {
                    case ConsoleKey.UpArrow:
                        if ( selectedIndex > 0 ) selectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if ( selectedIndex < properties.Count - 1 ) selectedIndex++;
                        break;
                    case ConsoleKey.Enter:
                        EditarPropriedade(selectedIndex);
                        break;
                    case ConsoleKey.A:
                        AdicionarPropriedade();
                        break;
                    case ConsoleKey.Delete:
                        RemoverPropriedade(selectedIndex);
                        break;
                    case ConsoleKey.S:
                        editando = false;
                        break;
                }
            }
        }

        private static void ExibirCabecalho(string entidade)
        {
            Console.WriteLine($"[✏️] Editando entidade: {entidade}.cs");
            Console.WriteLine("Use ↑ ↓ para navegar, Enter para editar, Del para remover, A para adicionar nova, S para salvar e sair.");
            Console.WriteLine();
        }

        private static void ExibirPropriedades()
        {
            for ( int i = 0; i < properties.Count; i++ )
            {
                var prop = properties[i];
                string linha = $"{i + 1}. {prop.Atributos} {prop.Tipo} {prop.Nome}";

                if ( i == selectedIndex )
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"> {linha}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("  " + linha);
                }
            }

            if ( properties.Count == 0 )
                Console.WriteLine("(sem propriedades ainda)");
        }

        private static void EditarPropriedade(int index)
        {
            var prop = properties[index];
            Console.Clear();
            Console.WriteLine("Editar Propriedade:");

            Console.Write($"Nome atual [{prop.Nome}]: ");
            string? nome = Console.ReadLine();
            if ( !string.IsNullOrWhiteSpace(nome) ) prop.Nome = nome;

            Console.Write($"Tipo atual [{prop.Tipo}]: ");
            string? tipo = Console.ReadLine();
            if ( !string.IsNullOrWhiteSpace(tipo) ) prop.Tipo = tipo;

            Console.Write($"Atributos (ex: [Required],[MaxLength(100)]) [{prop.Atributos}]: ");
            string? attr = Console.ReadLine();
            if ( !string.IsNullOrWhiteSpace(attr) ) prop.Atributos = attr;
        }

        private static void AdicionarPropriedade()
        {
            Console.Clear();
            Console.WriteLine("Nova Propriedade:");

            Console.Write("Nome: ");
            string? nome = Console.ReadLine();

            Console.Write("Tipo: ");
            string? tipo = Console.ReadLine();

            Console.Write("Atributos (ex: [Required],[MaxLength(100)]): ");
            string? attr = Console.ReadLine();

            if ( !string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(tipo) )
            {
                properties.Add(new EntityProperty
                {
                    Nome = nome,
                    Tipo = tipo,
                    Atributos = attr ?? string.Empty
                });
                selectedIndex = properties.Count - 1;
            }
        }

        private static void RemoverPropriedade(int index)
        {
            if ( index >= 0 && index < properties.Count )
            {
                properties.RemoveAt(index);
                if ( selectedIndex >= properties.Count )
                    selectedIndex = properties.Count - 1;
            }
        }
    }
}
