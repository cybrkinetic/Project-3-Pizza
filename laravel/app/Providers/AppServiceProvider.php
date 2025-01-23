<?php

namespace App\Providers;

use Illuminate\Support\ServiceProvider;
use Illuminate\Support\Facades\View;
use App\Models\BesteldePizza;

class AppServiceProvider extends ServiceProvider
{
    /**
     * Register any application services.
     */
    public function register(): void
    {
        //
    }

    /**
     * Bootstrap any application services.
     */
    public function boot(): void
    {
        View::composer('*', function ($view) {
            $cartItemCount = auth()->check() ? 
                BesteldePizza::where('userId', auth()->id())
                    ->whereNull('orderId')
                    ->count() : 0;
            $view->with('cartItemCount', $cartItemCount);
        });
    }
}
