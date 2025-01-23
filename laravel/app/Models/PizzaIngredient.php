<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class PizzaIngredient extends Model
{
    

    protected $table = 'pizzaingredient';
    protected $fillable =
        [
            'pizzaId',
            'ingredientId'
        ];
    protected $primaryKey = 'id';
    protected $keytype = 'int';
    public $timestamps = false;
}
