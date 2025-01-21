<?php

use App\Http\Controllers\ProfileController;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\MenuController;
use App\Http\Controllers\BestellenController;
use App\Http\Controllers\OrderlijstController;

Route::get('/', function () {
    return view('pizzeria.home');
});
Route::resource('menu', MenuController::class);
Route::resource('order', BestellenController::class);
Route::resource('orderlist', OrderlijstController::class);


Route::get('/search', function() {
    return view('pizzeria.search');
})->name('search');

Route::get('/register', function () {
    return view('auth.register');
})->name('register');

Route::get('/login', function () {
    return view('auth.login');
})->name('login');

Route::get('/dashboard', function () {
    return view('dashboard');
})->middleware(['auth', 'verified'])->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/profile', [ProfileController::class, 'edit'])->name('profile.edit');
    Route::patch('/profile', [ProfileController::class, 'update'])->name('profile.update');
    Route::delete('/profile', [ProfileController::class, 'destroy'])->name('profile.destroy');
});

require __DIR__.'/auth.php';
