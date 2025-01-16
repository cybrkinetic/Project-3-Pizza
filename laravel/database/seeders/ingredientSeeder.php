<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ingredientSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('ingredient')->insert([
            ['ingredientNaam' => 'Tomatensaus', 'unit' => '200 gram'],
            ['ingredientNaam' => 'Mozzarella', 'unit' => '100 gram'],
            ['ingredientNaam' => 'Mozzarella', 'unit' => '200 gram'],
            ['ingredientNaam' => 'Pizza kruiden', 'unit' => '100 gram'],
            ['ingredientNaam' => 'Pepperoni', 'unit' => '100 gram'],
            ['ingredientNaam' => 'Artisjok', 'unit' => '100 gram'],
            ['ingredientNaam' => 'Champignons', 'unit' => '150 gram'],
        ]);
        
    }
}
