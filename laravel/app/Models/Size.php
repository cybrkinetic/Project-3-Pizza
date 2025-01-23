<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Size extends Model
{

    protected $table = 'size';
    protected $fillable =
        [
            'grootte',
            'priceMultiplyer'
        ];
    protected $primaryKey = 'id';
    protected $keytype = 'int';
    public $timestamps = false;
}
