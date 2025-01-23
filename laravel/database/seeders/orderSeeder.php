<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class orderSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('ordertable')->insert([
            ['userId' => 1, 'statusId' => 1],
            ['userId' => 1, 'statusId' => 2],
            ['userId' => 2, 'statusId' => 1],
            ['userId' => 2, 'statusId' => 2],
            ['userId' => 3, 'statusId' => 1],
            ['userId' => 3, 'statusId' => 2],
        ]);

    }
}
