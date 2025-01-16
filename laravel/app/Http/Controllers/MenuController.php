<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Pizza;
use App\Models\Size;
use App\Models\BesteldePizza;

class MenuController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $pizzalijst = PizzaModel::all();
        $groottelijst = SizeModel::all();

    return view('menu', ['pizzalijst' => $pizzalijst , 'groottelijst' => $groottelijst]);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request){

        BesteldePizzaModel::create(
            [
            'orderId' => null,
            'pizzaId' => $request['PizzaID'],
            'sizeId' => $request['FormaatID'],
            'pizzaStatusId' => 6,
            'userId' => $request['UserID']
        ]);
        return redirect()->route('menu')
            ->with('success', 'Pizza added successfully.');
    }

}