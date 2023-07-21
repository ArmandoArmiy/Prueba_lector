using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf.parser;

class Program
{
    static void Main()
    {
        string pdfFilePath = "C:/Users/arman/OneDrive/Escritorio/TRFC/Archivos/443630400081.pdf";
        string txtFilePath = "C:/Users/arman/OneDrive/Escritorio/TRFC/Archivos/datos.txt";

        try
        {
            // Crear el lector de PDF
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                // Crear el escritor de texto
                using (StreamWriter writer = new StreamWriter(txtFilePath))
                {
                    // Recorrer las páginas del PDF
                    for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
                    {
                        // Extraer el contenido de la página
                        string pageText = PdfTextExtractor.GetTextFromPage(reader, pageNumber);
                        

                        // Filtrar y guardar solo las líneas deseadas
                        string filteredText = FilterText(pageText);

                        // Escribir el contenido filtrado en el archivo de texto
                        writer.WriteLine(filteredText);
                    }
                }
            }

            Console.WriteLine("Datos extraídos exitosamente y guardados en el archivo de texto.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error al leer el archivo PDF: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error inesperado: " + e.Message);
        }

        Console.ReadLine();
    }

    static string FilterText(string pageText)
    {
        // Aplica aquí la lógica para filtrar y seleccionar los fragmentos deseados
        string pattern = @"(?i)(kWh base|kWh intermedia|kWh punta|kW base|kW intermedia|kW punta|kWMax|kVArh|Distribución).*";
        string filteredText = string.Empty;

        MatchCollection matches = Regex.Matches(pageText, pattern, RegexOptions.Multiline);
        foreach (Match match in matches)
        {
            filteredText += match.Value + "\n";
        }

        return filteredText;
    }
}
