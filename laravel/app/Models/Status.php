<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Status extends Model
{

    protected $table = 'status';
    protected $fillable =
        [
            'soortStatus',
            'status'
        ];
    protected $primaryKey = 'id';
    protected $keytype = 'int';
    public $timestamps = false;
}
