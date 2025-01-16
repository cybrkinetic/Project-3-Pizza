using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.IO.Pipelines;
using StonksPizzaWPF.Views;

namespace StonksPizzaWPF.Models
{
    public class StonksPizzaDB
    {
        public const string OK = "OK";
        public const string ERROR = "Error";
        public const string UNKNOWN = "UNKNOWN";
        public const string NOTFOUND = "NOTFOUND";

        private string connString = ConfigurationManager.ConnectionStrings["pizzaConnection"].ConnectionString;

        /*
        static MySqlConnection mySqlConnection = null;
        public static MySqlConnection GetConnection()
        {
            if (mySqlConnection == null)
            {
                string conn = ConfigurationManager.ConnectionStrings["pizzaConnection"].ConnectionString;
                mySqlConnection = new MySqlConnection(conn);
            }
            return mySqlConnection;
        }
        */

        #region ingredients
        #region GetIngredients
        // GetIngredients leest alle rijen in uit de databasetabel ingredient en voegt deze toe aan ICollection.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetIngredients:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle ingredienten ingelezen)
        public string GetIngredients(ICollection<Ingredient> ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetIngredients");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `ingredientNaam`, `unit` 
                        FROM `ingredient`;
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Ingredient ingredient = new Ingredient()
                        {
                            Id = (ulong)reader["id"],
                            IngredientNaam = (string)reader["ingredientNaam"],
                            Unit = (string)reader["unit"],
                        };

                        ingredients.Add(ingredient);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetIngredients));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region CreateIngredient
        // CreateIngredient voegt het ingredient object uit de parameter toe aan de database.
        // Het ingredient object moet aan alle database eisen voldoen. De waarde van CreateIngredient:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreateIngredient(Ingredient ingredient)
        {
            if (ingredient == null || string.IsNullOrEmpty(ingredient.IngredientNaam)
            || ingredient.Unit == "")
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateIngredient");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        INSERT INTO `ingredient`
                        (`id`, `ingredientNaam`, `unit`) 
                        VALUES (NULL, @ingredientNaam, @unit)
                        ";
                    sql.Parameters.AddWithValue("@ingredientNaam", ingredient.IngredientNaam);
                    sql.Parameters.AddWithValue("@unit", ingredient.Unit);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {ingredient.IngredientNaam} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region UpdateIngredient
        // UpdateIngredient wijzigt het ingredient met id ingredientId (parameter) met de gegevens uit
        // de parameter ingredient. De gegevens van ingredient moeten aan alle database eisen voldoen.
        // De waarde van UpdateIngredient:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateIngredient(ulong ingredientId, Ingredient ingredient)
        {
            if (ingredient == null || string.IsNullOrEmpty(ingredient.IngredientNaam)
            || ingredient.Unit == "")
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateIngredient");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE `ingredient` 
                        SET `ingredientNaam`=@ingredientNaam,
                        `unit`= @unit
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@ingredientNaam", ingredient.IngredientNaam);
                    sql.Parameters.AddWithValue("@unit", ingredient.Unit);
                    sql.Parameters.AddWithValue("@id", ingredientId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {ingredient.IngredientNaam} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region DeleteIngredient
        // DeleteIngredient verwijdert het ingredient met de id ingredientId uit de database. De waarde
        // van DeleteIngredient :
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteIngredient(ulong ingredientId)
        {
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        DELETE 
                        FROM `ingredient` 
                        WHERE `id` = @id                            
                        ";
                    sql.Parameters.AddWithValue("@id", ingredientId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingredient met id {ingredientId} kon niet verwijderd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeleteIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetIngredient
        // GetIngredient leest 1 rij in uit de databasetabel Ingredients. Wordt er een rij gevonden, dan
        // worden de gegevens hiervan in de output parameter ingredient gezet.
        // Parameters:
        // - ingredientId : Id van het in te lezen ingredient
        // - ingredient(o) : null = niet gevonden
        // anders nieuw ingredient object met de database gegevens
        // De waarde van GetIngredient:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten ware
        public string GetIngredient(ulong ingredientId, out Ingredient? ingredient)
        {
            ingredient = null;
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `ingredientNaam`, `unit` 
                        FROM `ingredient` 
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@id", ingredientId);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        ingredient = new()
                        {
                            Id = (ulong)reader["id"],
                            IngredientNaam = (string)reader["ingredientNaam"],
                            Unit = (string)reader["unit"]
                        };
                    }
                    methodResult = ingredient == null ? NOTFOUND : OK;
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #endregion


        #region pizzas
        #region GetPizzas
        // GetPizzas leest alle rijen in uit de databasetabel pizza en voegt deze toe aan ICollection.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetPizzas:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle pizzas ingelezen)
        public string GetPizzas(ICollection<Pizza> pizzas)
        {
            if (pizzas == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetPizzas");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `pizzaNaam`, `pizzaPrijs` 
                        FROM `pizza`
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Pizza pizza = new Pizza()
                        {
                            Id = (ulong)reader["id"],
                            PizzaNaam = (string)reader["pizzaNaam"],
                            PizzaPrijs = (decimal)reader["pizzaPrijs"],
                        };

                        pizzas.Add(pizza);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetPizzas));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region CreatePizza
        // CreatePizza voegt het pizza object uit de parameter toe aan de database.
        // Het pizza object moet aan alle database eisen voldoen. De waarde van CreatePizza:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreatePizza(Pizza pizza)
        {
            if (pizza == null || string.IsNullOrEmpty(pizza.PizzaNaam))
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreatePizza");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        INSERT INTO `pizza`
                        (`id`, `pizzaNaam`, `pizzaPrijs`) 
                        VALUES (NULL, @pizzaNaam, @pizzaPrijs)
                        ";
                    sql.Parameters.AddWithValue("@pizzaNaam", pizza.PizzaNaam);
                    sql.Parameters.AddWithValue("@pizzaPrijs", pizza.PizzaPrijs);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Pizza {pizza.PizzaNaam} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreatePizza));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region UpdatePizza
        // UpdatePizza wijzigt het pizza met id pizzaId (parameter) met de gegevens uit
        // de parameter pizza. De gegevens van pizza moeten aan alle database eisen voldoen.
        // De waarde van UpdatePizza:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdatePizza(ulong pizzaId, Pizza pizza)
        {
            if (pizza == null || string.IsNullOrEmpty(pizza.PizzaNaam))
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdatePizza");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE `pizza` 
                        SET `pizzaNaam`= @pizzaNaam,
                        `pizzaPrijs`= @pizzaPrijs 
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@pizzaNaam", pizza.PizzaNaam);
                    sql.Parameters.AddWithValue("@pizzaPrijs", pizza.PizzaPrijs);
                    sql.Parameters.AddWithValue("@id", pizzaId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {pizza.PizzaNaam} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdatePizza));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region DeletePizza
        // DeletePizza verwijdert het pizza met de id pizzaId uit de database. De waarde
        // van DeletePizza :
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeletePizza(ulong pizzaId)
        {
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        DELETE 
                        FROM `pizza` 
                        WHERE `id` = @id                     
                        ";
                    sql.Parameters.AddWithValue("@id", pizzaId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Pizza met id {pizzaId} kon niet verwijderd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeletePizza));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetPizza
        // GetPizza leest 1 rij in uit de databasetabel Pizzas. Wordt er een rij gevonden, dan
        // worden de gegevens hiervan in de output parameter pizza gezet.
        // Parameters:
        // - pizzaId : Id van het in te lezen pizza
        // - pizza(o) : null = niet gevonden
        // anders nieuw pizza object met de database gegevens
        // De waarde van GetPizza:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten ware
        public string GetPizza(ulong pizzaId, out Pizza? pizza)
        {
            pizza = null;
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `pizzaNaam`, `pizzaPrijs` 
                        FROM `pizza` 
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@id", pizzaId);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        pizza = new()
                        {
                            Id = (ulong)reader["id"],
                            PizzaNaam = (string)reader["pizzaNaam"],
                            PizzaPrijs = (decimal)reader["pizzaPrijs"],
                        };
                    }
                    methodResult = pizza == null ? NOTFOUND : OK;
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetPizza));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #endregion

        #region pizzaIngredient
        #region GetAddedPizzaIngredients
        // GetAddedPizzaIngredients leest alle rijen in uit de databasetabel PizzaIngredient en voegt deze toe aan ICollection als ze bij de pizza horen.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetAddedPizzaIngredients:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle pizzaIngredienten ingelezen)
        public string GetAddedPizzaIngredients(ICollection<Ingredient> pizzaIngredients, ulong pizzaId)
        {
            if (pizzaIngredients == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetAddedPizzaIngredients");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT ingredient.id, ingredient.ingredientNaam, ingredient.unit 
                        FROM `pizzaIngredient` 
                        INNER JOIN ingredient ON pizzaIngredient.ingredientId = ingredient.id 
                        WHERE pizzaId = @id
                    ";
                    sql.Parameters.AddWithValue("@id", pizzaId);
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Ingredient pizzaIngredient = new Ingredient()
                        {
                            Id = (ulong)reader["id"],
                            IngredientNaam = (string)reader["ingredientNaam"],
                            Unit = (string)reader["unit"],
                        };

                        pizzaIngredients.Add(pizzaIngredient);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetAddedPizzaIngredients));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetNotAddedPizzaIngredients
        // GetNotAddedPizzaIngredients leest alle rijen in uit de databasetabel PizzaIngredient en voegt deze toe aan ICollection als ze niet bij de pizza horen.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetNotAddedPizzaIngredients:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle pizzaIngredienten ingelezen)
        public string GetNotAddedPizzaIngredients(ICollection<Ingredient> pizzaIngredients, ulong pizzaId)
        {
            if (pizzaIngredients == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetNotAddedPizzaIngredients");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT DISTINCT ingredient.id, ingredient.ingredientNaam, ingredient.unit 
                        FROM `ingredient` 
                        LEFT JOIN pizzaIngredient ON ingredient.id = pizzaIngredient.ingredientId 
                        WHERE ingredient.id NOT IN 
                        (
                        SELECT DISTINCT ingredientId 
                        FROM `ingredient` 
                        INNER JOIN pizzaIngredient ON ingredient.id = pizzaIngredient.ingredientId 
                        WHERE pizzaIngredient.pizzaId = 5
                        )


                    ";
                    sql.Parameters.AddWithValue("@id", pizzaId);
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {

                        Ingredient pizzaIngredient = new Ingredient()
                        {
                            Id = (ulong)reader["id"],
                            IngredientNaam = (string)reader["ingredientNaam"],
                            Unit = (string)reader["unit"],
                        };

                        pizzaIngredients.Add(pizzaIngredient);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetNotAddedPizzaIngredients));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region CreatePizzaIngredient
        // CreatePizzaIngredient voegt het pizzaIngredient object uit de parameter toe aan de database.
        // Het pizzaIngredient object moet aan alle database eisen voldoen. De waarde van CreatePizzaIngredient:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreatePizzaIngredient(ulong ingredientId, ulong pizzaId)
        {
            if (ingredientId == null || pizzaId == null )
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreatePizzaIngredient");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        INSERT INTO `pizzaIngredient`
                        (`id`, `pizzaId`, `ingredientId`) 
                        VALUES (NULL, @pizzaId, @ingredientId)

                        ";
                    sql.Parameters.AddWithValue("@pizzaId", pizzaId);
                    sql.Parameters.AddWithValue("@ingredientId", ingredientId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"PizzaIngrediënt kon niet aan pizza toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreatePizzaIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region DeletePizzaIngredient
        // DeletePizzaIngredient verwijdert het pizzaingredient met de id pizzaingredientId uit de database. De waarde
        // van DeletePizzaIngredient :
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeletePizzaIngredient(ulong ingredientId, ulong pizzaId)
        {
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        DELETE 
                        FROM `pizzaIngredient` 
                        WHERE pizzaId = @pizzaId AND ingredientId = @ingredientId                           
                        ";
                    sql.Parameters.AddWithValue("@pizzaId", pizzaId);
                    sql.Parameters.AddWithValue("@ingredientId", ingredientId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"PizzaIngredient met id {ingredientId} kon niet verwijderd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeletePizzaIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #endregion

        #region besteldePizza
        #region GetBesteldePizzas
        // GetBesteldePizzas leest alle rijen in uit de databasetabel besteldePizza en voegt deze toe aan ICollection.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetBesteldePizzas:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle besteldepizzas ingelezen)
        public string GetBesteldePizzas(ICollection<BesteldePizza> besteldepizzas)
        {
            if (besteldepizzas == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetBesteldePizzas");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT besteldepizza.id, besteldepizza.orderId, besteldepizza.pizzaId, besteldepizza.sizeId, besteldepizza.pizzaStatusId, 
                            pizza.id, pizza.pizzaNaam, pizza.pizzaPrijs, 
                            size.id, size.grootte, size.priceMultiplyer, 
                            status.id, status.soortStatus, status.status 
                        FROM `besteldepizza` 
                        INNER JOIN pizza ON besteldepizza.pizzaId = pizza.id 
                        INNER JOIN size ON besteldepizza.sizeId = size.id 
                        INNER JOIN status ON besteldepizza.pizzaStatusId = status.id;
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        BesteldePizza besteldePizza = new BesteldePizza()
                        {
                            Id = (ulong)reader["besteldepizza.id"],
                            OrderId = (ulong)reader["besteldepizza.orderId"],
                            PizzaId = (ulong)reader["besteldepizza.pizzaId"],
                            SizeId = (ulong)reader["besteldepizza.sizeId"],
                            PizzaStatusId = (ulong)reader["besteldepizza.pizzaStatusId"],
                            Pizza = new Pizza()
                            {
                                Id = (ulong)reader["pizza.id"],
                                PizzaNaam = (string)reader["pizza.pizzaNaam"],
                                PizzaPrijs = (decimal)reader["pizza.pizzaPrijs"],
                            },
                            Size = new Size()
                            {
                                Id = (ulong)reader["size.id"],
                                Grootte = (string)reader["size.grootte"],
                                PriceMultiplyer = (decimal)reader["size.priceMultiplyer"],                               
                            },
                            Status = new Status()
                            {
                                Id = (ulong)reader["status.id"],
                                SoortStatus = (int)reader["status.soortStatus"],
                                StatusString = (string)reader["status.status"],
                            },
                        
                        };

                        besteldepizzas.Add(besteldePizza);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetBesteldePizzas));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region UpdateBesteldePizzaStatus
        // UpdateBesteldePizzaStatus wijzigt het besteldepizza met id pizzaId (parameter) met de gegevens uit
        // de parameter besteldepizza. De gegevens van besteldepizza moeten aan alle database eisen voldoen.
        // De waarde van UpdatBesteldePizzas:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateBesteldePizza(ulong besteldepizzaId, BesteldePizza Besteldepizza)
        {
            if (Besteldepizza == null || string.IsNullOrEmpty(Besteldepizza.Pizza.PizzaNaam))
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateBesteldePizza");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE `besteldepizza` 
                        SET `pizzaStatusId`= @statusId
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@statusId", Besteldepizza.PizzaStatusId);
                    sql.Parameters.AddWithValue("@id", besteldepizzaId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {Besteldepizza.Pizza.PizzaNaam} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateBesteldePizza));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetBesteldePizzasByOrderId
        // GetBesteldePizzas leest 1 rij in uit de databasetabel Pizzas. Wordt er een rij gevonden, dan
        // worden de gegevens hiervan in de output parameter pizza gezet.
        // Parameters:
        // - pizzaId : Id van het in te lezen pizza
        // - pizza(o) : null = niet gevonden
        // anders nieuw pizza object met de database gegevens
        // De waarde van GeBesteldePizzas:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten ware
        public string GetBesteldePizzas(ulong OrderId, ICollection<BesteldePizza> besteldepizzas)
        {
            if (besteldepizzas == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetBesteldePizzas");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT besteldepizza.id, besteldepizza.orderId, besteldepizza.pizzaId, besteldepizza.sizeId, besteldepizza.pizzaStatusId, 
                            pizza.id, pizza.pizzaNaam, pizza.pizzaPrijs, 
                            size.id, size.grootte, size.priceMultiplyer, 
                            status.id, status.soortStatus, status.status 
                        FROM `besteldepizza` 
                        INNER JOIN pizza ON besteldepizza.pizzaId = pizza.id 
                        INNER JOIN size ON besteldepizza.sizeId = size.id 
                        INNER JOIN status ON besteldepizza.pizzaStatusId = status.id
                        WHERE besteldepizza.orderId = @id
                    ";

                    sql.Parameters.AddWithValue("@id", OrderId);
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        BesteldePizza besteldePizza = new BesteldePizza()
                        {
                            Id = (ulong)reader["id"],
                            OrderId = (ulong)reader["orderId"],
                            PizzaId = (ulong)reader["pizzaId"],
                            SizeId = (ulong)reader["sizeId"],
                            PizzaStatusId = (ulong)reader["pizzaStatusId"],
                            Pizza = new Pizza()
                            {
                                Id = (ulong)reader["id"],
                                PizzaNaam = (string)reader["pizzaNaam"],
                                PizzaPrijs = (decimal)reader["pizzaPrijs"],
                            },
                            Size = new Size()
                            {
                                Id = (ulong)reader["id"],
                                Grootte = (string)reader["grootte"],
                                PriceMultiplyer = (decimal)reader["priceMultiplyer"],
                            },
                            Status = new Status()
                            {
                                Id = (ulong)reader["id"],
                                SoortStatus = (int)reader["soortStatus"],
                                StatusString = (string)reader["status"],
                            },

                        };

                        besteldepizzas.Add(besteldePizza);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetBesteldePizzas));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #endregion

        #region order
        #region GetOrders
        // GetOrders leest alle rijen in uit de databasetabel orders en voegt deze toe aan ICollection.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetOrders:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle orders ingelezen)
        public string GetOrders(ICollection<Order> orders)
        {
            if (orders == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetOrders");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT ordertable.id, ordertable.statusId, 
                            status.id, status.soortStatus, status.status 
                        FROM `ordertable` 
                        INNER JOIN status ON ordertable.statusId = status.id
                        ORDER BY ordertable.id ASC
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Order order = new Order()
                        {
                            Id = (ulong)reader["id"],
                            StatusId = (ulong)reader["statusId"],
                            Status = new Status
                            {
                                Id = (ulong)reader["id"],
                                SoortStatus = (int)reader["soortStatus"],
                                StatusString = (string)reader["status"],
                            },
                        };

                        orders.Add(order);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetOrders));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region UpdateOrder
        // UpdateOrder wijzigt het order met id orderId (parameter) met de gegevens uit
        // de parameter order. De gegevens van order moeten aan alle database eisen voldoen.
        // De waarde van UpdateOrder:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateOrder(ulong orderId, Order order)
        {
            if (order == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateOrder");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE `ordertable` 
                        SET `statusId`=  @statusId
                        WHERE `id` =  @id
                        ";
                    sql.Parameters.AddWithValue("@statusId", order.StatusId);
                    sql.Parameters.AddWithValue("@id", orderId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Order {order.Id} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateOrder));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetOrder
        // GetOrder leest 1 rij in uit de databasetabel Orders. Wordt er een rij gevonden, dan
        // worden de gegevens hiervan in de output parameter order gezet.
        // Parameters:
        // - orderId : Id van het in te lezen order
        // - order(o) : null = niet gevonden
        // anders nieuw order object met de database gegevens
        // De waarde van GetOrder:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten ware
        public string GetOrder(ulong orderId, out Order? order)
        {
            order = null;
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT ordertable.id, ordertable.statusId, 
                            status.id, status.soortStatus, status.status 
                        FROM `ordertable` 
                        INNER JOIN status ON ordertable.statusId = status.id 
                        WHERE ordertable.id = @id
                        ";
                    sql.Parameters.AddWithValue("@id", orderId);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        order = new()
                        {
                            Id = (ulong)reader["ordertable.id"],
                            StatusId = (ulong)reader["ordertable.statusId"],
                            Status = new Status
                            {
                                Id = (ulong)reader["status.id"],
                                SoortStatus = (int)reader["status.soortStatus"],
                                StatusString = (string)reader["status.status"],
                            },
                        };
                    }
                    methodResult = order == null ? NOTFOUND : OK;
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetOrder));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #endregion
        


        #region size    
        #region GetSizes
        // GetSizes leest alle rijen in uit de databasetabel ingredient en voegt deze toe aan ICollection.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetSizes:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle ingredienten ingelezen)
        public string GetSizes(ICollection<Size> sizes)
        {
            if (sizes == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetSizes");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();    
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `grootte`, `priceMultiplyer` 
                        FROM `size`
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Size size = new Size()
                        {
                            Id = (ulong)reader["id"],
                            Grootte = (string)reader["grootte"],
                            PriceMultiplyer = (decimal)reader["priceMultiplyer"],
                        };

                        sizes.Add(size);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetSizes));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region CreateSize
        // CreateSize voegt het ingredient object uit de parameter toe aan de database.
        // Het size object moet aan alle database eisen voldoen. De waarde van CreateSize:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreateSize(Size size)
        {
            if (size == null || string.IsNullOrEmpty(size.Grootte))
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateSize");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        INSERT INTO `size`
                        (`id`, `grootte`, `priceMultiplyer`) 
                        VALUES (NULL, @grootte, @priceMultiplyer)
                        ";
                    sql.Parameters.AddWithValue("@priceMultiplyer", size.PriceMultiplyer);
                    sql.Parameters.AddWithValue("@grootte", size.Grootte);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Size {size.Grootte} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateSize));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region UpdateSize
        // UpdateSize wijzigt het size met id sizeId (parameter) met de gegevens uit
        // de parameter size. De gegevens van size moeten aan alle database eisen voldoen.
        // De waarde van UpdateSize:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateSize(ulong sizeId, Size size)
        {
            if (size == null || string.IsNullOrEmpty(size.Grootte))
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateSize");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE `size` 
                        SET `grootte`= @grootte,`priceMultiplyer`= @priceMultiplyer 
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@grootte", size.Grootte);
                    sql.Parameters.AddWithValue("@priceMultiplyer", size.PriceMultiplyer);
                    sql.Parameters.AddWithValue("@id", sizeId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Size {size.Grootte} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateSize));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region DeleteSize
        // DeleteSize verwijdert het size met de id sizeId uit de database. De waarde
        // van DeleteSize :
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteSize(ulong sizeId)
        {
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                    DELETE 
                    FROM `size` 
                    WHERE `id` = @id
                    ";
                    sql.Parameters.AddWithValue("@id", sizeId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Size met id {sizeId} kon niet verwijderd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeleteSize));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetSize
        // GetSize leest 1 rij in uit de databasetabel Sizes. Wordt er een rij gevonden, dan
        // worden de gegevens hiervan in de output parameter size gezet.
        // Parameters:
        // - sizeId : Id van het in te lezen size
        // - size(o) : null = niet gevonden
        // anders nieuw size object met de database gegevens
        // De waarde van GetSize:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten ware
        public string GetSize(ulong sizeId, out Size? size)
        {
            size = null;
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `grootte`, `priceMultiplyer` 
                        FROM `size` 
                        WHERE `id` =  @id
                        ";
                    sql.Parameters.AddWithValue("@id", sizeId);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        size = new()
                        {
                            Id = (ulong)reader["id"],
                            Grootte = (string)reader["grootte"],
                            PriceMultiplyer = (decimal)reader["priceMultiplyer"]
                        };
                    }
                    methodResult = size == null ? NOTFOUND : OK;
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetSize));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion    
        #endregion

        #region status     
        #region GetStatusPizza
        // GetStatus leest alle rijen in uit de databasetabel status en voegt deze toe aan ICollection.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetStatus:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle statuses ingelezen)
        public string GetStatusPizza(ICollection<Status> statuses)
        {
            if (statuses == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetStatus");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `soortStatus`, `status` 
                        FROM `status`
                        WHERE soortStatus = 2
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Status status = new Status()
                        {
                            Id = (ulong)reader["id"],
                            SoortStatus = (int)reader["soortStatus"],
                            StatusString = (string)reader["status"],
                        };

                        statuses.Add(status);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetStatus));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetStatusOrder
        // GetStatus leest alle rijen in uit de databasetabel status en voegt deze toe aan ICollection.
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetStatus:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten waren (mogelijk zijn niet alle statuses ingelezen)
        public string GetStatusOrder(ICollection<Status> statuses)
        {
            if (statuses == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetStatus");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `soortStatus`, `status` 
                        FROM `status`
                        WHERE soortStatus = 1

                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Status status = new Status()
                        {
                            Id = (ulong)reader["id"],
                            SoortStatus = (int)reader["soortStatus"],
                            StatusString = (string)reader["status"],
                        };

                        statuses.Add(status);
                    }

                    methodResult = "OK";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetStatus));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region CreateStatus
        // CreateStatus voegt het status object uit de parameter toe aan de database.
        // Het status object moet aan alle database eisen voldoen. De waarde van CreateStatus:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreateStatus(Status status)
        {
            if (status == null || string.IsNullOrEmpty(status.StatusString))
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateStatus");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        INSERT INTO `status`
                        (`id`, `soortStatus`, `status`) 
                        VALUES (NULL, @soortStatus, @status)
                        ";
                    sql.Parameters.AddWithValue("@soortStatus", status.SoortStatus);
                    sql.Parameters.AddWithValue("@status", status.StatusString);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Status {status.StatusString} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateStatus));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region UpdateStatus
        // UpdateStatus wijzigt het status met id statusId (parameter) met de gegevens uit
        // de parameter status. De gegevens van status moeten aan alle database eisen voldoen.
        // De waarde van UpdateStatus:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateStatus(ulong statusId, Status status)
        {
            if (status == null || string.IsNullOrEmpty(status.StatusString))
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateStatus");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE `status` 
                        SET `soortStatus`= @soortStatus,
                        `status`= @status 
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@soortStatus", status.SoortStatus);
                    sql.Parameters.AddWithValue("@status", status.StatusString);
                    sql.Parameters.AddWithValue("@id", statusId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Status {status.StatusString} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateStatus));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region DeleteStatus
        // DeleteStatus verwijdert het status met de id statusId uit de database. De waarde
        // van DeleteStatus :
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteStatus(ulong statusId)
        {
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        DELETE 
                        FROM `status` 
                        WHERE `id` = @id                         
                        ";
                    sql.Parameters.AddWithValue("@id", statusId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Status met id {statusId} kon niet verwijderd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeleteStatus));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
        #region GetStatus
        // GetStatus leest 1 rij in uit de databasetabel Status. Wordt er een rij gevonden, dan
        // worden de gegevens hiervan in de output parameter status gezet.
        // Parameters:
        // - statusId : Id van het in te lezen status
        // - status(o) : null = niet gevonden
        // anders nieuw status object met de database gegevens
        // De waarde van GetStatus:
        // - "ok" als er geen fouten waren.
        // - een foutmelding, als er wel fouten ware
        public string GetStatus(ulong statusId, out Status? status)
        {
            status = null;
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT `id`, `soortStatus`, `status` 
                        FROM `status` 
                        WHERE `id` = @id
                        ";
                    sql.Parameters.AddWithValue("@id", statusId);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        status = new()
                        {
                            Id = (ulong)reader["id"],
                            SoortStatus = (int)reader["soortStatus"],
                            StatusString = (string)reader["status"]
                        };
                    }
                    methodResult = status == null ? NOTFOUND : OK;
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetStatus));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion  
        #endregion






        #region UpdateOrderStatus
        // UpdateOrder wijzigt het order met id orderId (parameter) met de gegevens uit
        // de parameter order. De gegevens van order moeten aan alle database eisen voldoen.
        // De waarde van UpdateOrder:
        // - "ok" als er geen fouten waren.
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateOrderStatus(ulong orderId, Order order)
        {
            if (order == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateOrder");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE `ordertable` 
                        SET `statusId`=  @statusId
                        WHERE `id` =  @id
                        ";
                    sql.Parameters.AddWithValue("@statusId", order.StatusId);
                    sql.Parameters.AddWithValue("@id", orderId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Order {order.Id} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateOrder));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion



    }
}
