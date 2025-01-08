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
        Schema::create('besteldepizza', function (Blueprint $table) {
            $table->id();
            $table->unsignedBigInteger('orderId')->nullable()->length(11);
            $table->foreign('orderId')->references('id')->on('ordertable')->onDelete('cascade');
            $table->unsignedBigInteger('pizzaId')->length(11);
            $table->foreign('pizzaId')->references('id')->on('pizza')->onDelete('cascade');
            $table->unsignedBigInteger('sizeId')->length(11);
            $table->foreign('sizeId')->references('id')->on('size')->onDelete('cascade');
            $table->unsignedBigInteger('pizzaStatusId')->length(11);
            $table->foreign('pizzaStatusId')->references('id')->on('status')->onDelete('cascade');
            $table->unsignedBigInteger('userId')->length(11);
            $table->foreign('userId')->references('id')->on('users')->onDelete('cascade');
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('besteldepizza');
    }
};
