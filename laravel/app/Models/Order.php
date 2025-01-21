<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Order extends Model
{

    protected $table = 'ordertable';
    protected $fillable =
        [
            'statusId'
        ];
    protected $primaryKey = 'id';
    protected $keytype = 'int';
    public $timestamps = false;

    public function OrderStatus(): HasOne
    {
        return $this->hasOne(Status::class, 'id' ,'statusId');
    }
    public function OrderPizzas(): HasMany
    {
        return $this->hasMany(BesteldePizza::class, 'orderId' ,'id');
    }
}
