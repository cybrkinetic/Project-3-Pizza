<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class pizzaIngredientSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('pizzaingredient')->insert([
            ['pizzaId' => 1, 'ingredientId' => 1],
            ['pizzaId' => 1, 'ingredientId' => 3],
            ['pizzaId' => 1, 'ingredientId' => 4],
            ['pizzaId' => 2, 'ingredientId' => 1],
            ['pizzaId' => 2, 'ingredientId' => 2],
            ['pizzaId' => 2, 'ingredientId' => 5],
            ['pizzaId' => 3, 'ingredientId' => 1],
            ['pizzaId' => 3, 'ingredientId' => 3],
            ['pizzaId' => 3, 'ingredientId' => 6],
            ['pizzaId' => 4, 'ingredientId' => 1],
            ['pizzaId' => 4, 'ingredientId' => 3],
            ['pizzaId' => 4, 'ingredientId' => 7],
        ]);
    
    }
}
