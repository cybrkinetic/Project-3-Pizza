<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Ingredient extends Model
{
    

    protected $table = 'ingredient';
    protected $fillable =
        [
            'ingredientNaam',
            'unit'
        ];
    protected $primaryKey = 'id';
    protected $keytype = 'int';
    public $timestamps = false;
}
