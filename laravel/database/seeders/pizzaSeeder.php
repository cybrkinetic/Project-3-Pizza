<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class pizzaSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('pizza')->insert([
            ['pizzaNaam' => 'Pizza Margaritha','pizzaPrijs' => 8.50],
            ['pizzaNaam' => 'Pizza Pepperoni', 'pizzaPrijs' => 9.00],
            ['pizzaNaam' => 'Pizza Funghi', 'pizzaPrijs' => 9.95],
            ['pizzaNaam' => 'Pizza Carciofi', 'pizzaPrijs' => 11.49],
        ]);
    }
}
