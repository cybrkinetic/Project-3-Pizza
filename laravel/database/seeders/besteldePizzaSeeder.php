<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class besteldePizzaSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('besteldepizza')->insert([
            ['orderId' => 1, 'pizzaId' => 1, 'sizeId' => 2, 'pizzaStatusId' => 6, 'userId' => 1],
            ['orderId' => 2, 'pizzaId' => 2, 'sizeId' => 3, 'pizzaStatusId' => 6, 'userId' => 1],
            ['orderId' => 2, 'pizzaId' => 3, 'sizeId' => 1, 'pizzaStatusId' => 6, 'userId' => 1],
            ['orderId' => 1, 'pizzaId' => 4, 'sizeId' => 2, 'pizzaStatusId' => 6, 'userId' => 1],
            ['orderId' => 3, 'pizzaId' => 1, 'sizeId' => 2, 'pizzaStatusId' => 6, 'userId' => 2],
            ['orderId' => 3, 'pizzaId' => 2, 'sizeId' => 3, 'pizzaStatusId' => 6, 'userId' => 2],
            ['orderId' => 3, 'pizzaId' => 1, 'sizeId' => 1, 'pizzaStatusId' => 6, 'userId' => 2],
            ['orderId' => 5, 'pizzaId' => 1, 'sizeId' => 2, 'pizzaStatusId' => 6, 'userId' => 3],
            ['orderId' => 5, 'pizzaId' => 2, 'sizeId' => 3, 'pizzaStatusId' => 6, 'userId' => 3],
            ['orderId' => 6, 'pizzaId' => 1, 'sizeId' => 1, 'pizzaStatusId' => 6, 'userId' => 3],
        ]);
       
        
    }
}
