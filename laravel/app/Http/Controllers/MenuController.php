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
        $pizzalijst = Pizza::all();
        $groottelijst = Size::all();

    return view('pizzeria.menu', ['pizzalijst' => $pizzalijst , 'groottelijst' => $groottelijst]);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request){

        if (!auth()->check()) {
            return redirect()->route('register')->with('message', 'Please register or log in to place an order.');
        }
        BesteldePizza::create(
            [
            'orderId' => null,
            'pizzaId' => $request['PizzaID'],
            'sizeId' => $request['FormaatID'],
            'pizzaStatusId' => 6,
            'userId' => $request['UserID']
        ]);
        return redirect()->route('menu.index')
            ->with('success', 'Pizza added successfully.');
    }

}