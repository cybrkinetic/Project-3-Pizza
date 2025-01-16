<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class sizeSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('size')->insert([
            ['grootte' => 'small', 'priceMultiplyer' => 0.80],
            ['grootte' => 'medium', 'priceMultiplyer' => 1.00],
            ['grootte' => 'large', 'priceMultiplyer' => 1.20],
        ]);
    }
}
