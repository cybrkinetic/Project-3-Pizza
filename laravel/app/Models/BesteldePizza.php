<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;


class BesteldePizza extends Model
{
  

    protected $table = 'besteldepizza';
    protected $fillable =
        [
            'orderId',
            'pizzaId',
            'sizeId',
            'pizzaStatusId',
            'userId'
        ];
    protected $primaryKey = 'id';
    protected $keytype = 'int';
    public $timestamps = false;

    public function PizzaNaam(): HasOne
    {
        return $this->hasOne(Pizza::class, 'id' ,'pizzaId');
    }
    public function PizzaSize(): HasOne
    {
        return $this->hasOne(Size::class, 'id' ,'sizeId');
    }
    public function PizzaStatus(): HasOne
    {
        return $this->hasOne(Status::class, 'id' ,'pizzaStatusId');
    }
}
