<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Http;
use App\Models\BesteldePizza;
use App\Models\Order;
use App\Models\Pizza;
use App\Models\Size;
use App\Models\Status;
use App\Models\User;

class BestellenController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $besteldePizzas = BesteldePizza::all()->where('userId', '=', auth()->id())->where('orderId', '=', NULL);
        $totaalPrijs = 0;
        foreach($besteldePizzas as $besteldePizza)
        {
            $prijs = $besteldePizza->PizzaNaam->pizzaPrijs;
            $multiply = $besteldePizza->PizzaSize->priceMultiplyer;
            $pizzaPrijs = $prijs * $multiply;
            $totaalPrijs += $pizzaPrijs;

        }
        //dd($totaalPrijs);
/*
        $enkelepizza = $besteldePizzas;

        $pizzaNaam = $enkelepizza->PizzaNaam;*/
        //BesteldePizzaModel;
        // $user_id = auth()->id();
        // $user_id = 1;
        // $besteldePizzas = DB::table('besteldepizza')->get();
        return view('pizzeria.order', compact('besteldePizzas', 'totaalPrijs'));

        // $besteldepizzalijst = BesteldePizzaModel::
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(string $id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy($id)
    {
        $besteldePizza = BesteldePizza::find($id);
        $besteldePizza->delete();

        return redirect()->route('order.index')
            ->with('success', 'Pizza removed successfully');
    }
}
