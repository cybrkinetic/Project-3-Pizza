<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('pizzaingredient', function (Blueprint $table) {
            $table->id();
            $table->unsignedBigInteger('pizzaId')->length(11);
            $table->foreign('pizzaId')->references('id')->on('pizza')->onDelete('cascade');
            $table->unsignedBigInteger('ingredientId')->length(11);
            $table->foreign('ingredientId')->references('id')->on('ingredient')->onDelete('cascade');
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('pizzaingredient');
    }
};
