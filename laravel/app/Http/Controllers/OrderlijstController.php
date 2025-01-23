<?php

namespace App\Http\Controllers;

use App\Models\BesteldePizza;
use Illuminate\Http\Request;
use App\Models\Order;

class OrderlijstController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $orderlijst = Order::where('userId', auth()->id())->get();

    return view('pizzeria.orderlist', ['orderlijst' => $orderlijst]);
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
    public function store(Request $request){

        $order = new Order;
        $order->statusId = 1;
        $order->userId = auth()->id();
        $order->save();

        $id = $order->id;
        $nieuweOrder = BesteldePizza::where('orderId', '=' , null)->get();

        foreach ($nieuweOrder as $aangemaakteOrder){
            $aangemaakteOrder->orderId = $id;
            $aangemaakteOrder->save();
        }
        return redirect()->route('orderlist.index');
    }

    /**
     * Display the specified resource.
     */
    public function show($id)
    {

        $besteldePizzas = BesteldePizza::all()->where('orderId', '=', $id);
        $order = Order::find($id);
        $totaalPrijs = 0;
        foreach($besteldePizzas as $besteldePizza)
        {
            $prijs = $besteldePizza->PizzaNaam->pizzaPrijs;
            $multiply = $besteldePizza->PizzaSize->priceMultiplyer;
            $pizzaPrijs = $prijs * $multiply;
            $totaalPrijs += $pizzaPrijs;

        }

        return view('pizzeria.orderShow', compact('besteldePizzas', 'totaalPrijs', 'order'));

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
        $order = Order::find($id);
        $order->statusId = 10;
        $order->save();

        return redirect()->route('orderlist')
            ->with('success', 'Bestelling verwijderd.');
    }
}
