<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Pizza extends Model
{

    protected $table = 'pizza';
    protected $fillable =
        [
            'pizzaNaam',
            'pizzaPrijs'
        ];
    protected $primaryKey = 'id';
    protected $keytype = 'int';
    public $timestamps = false;

    public function Ingredients(): BelongsToMany
    {
        return $this->belongsToMany(Ingredient::class,'pizzaingredient' ,'pizzaId' ,'ingredientId' );
    }
}
