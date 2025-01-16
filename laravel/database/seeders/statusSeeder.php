<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class statusSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('status')->insert([
            ['soortStatus' => 1, 'status' => 'Niet begonnen'],
            ['soortStatus' => 1, 'status' => 'Bezig met bestelling'],
            ['soortStatus' => 1, 'status' => 'Bestelling is compleet'],
            ['soortStatus' => 1, 'status' => 'Bestelling is onderweg'],
            ['soortStatus' => 1, 'status' => 'Afgerond'],
            ['soortStatus' => 2, 'status' => 'Niet begonnen'],
            ['soortStatus' => 2, 'status' => 'Pizza wordt voorbereid'],
            ['soortStatus' => 2, 'status' => 'Pizza is in oven'],
            ['soortStatus' => 2, 'status' => 'Pizza is klaar'],
            ['soortStatus' => 1, 'status' => 'Geannuleerd'],
        ]);
        
    
    }
}
