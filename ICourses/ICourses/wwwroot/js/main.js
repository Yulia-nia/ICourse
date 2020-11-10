$( document ).ready(function() {
    // Mobile menu
    $('.menu__close').click(function() {
        $('.header__menu').toggleClass('active');
        $('section').toggleClass('active');
    });
    $(window).resize(function() {
        if($(window).width() > 720) {
            $('.header__menu').removeClass('active');
            $('section').removeClass('active');
        }
    });

    // Header fixed
    $(window).scroll(function() {
        if($(window).scrollTop() > 0) {
            $('.header-fixed').addClass('fixed');
        } else {
            $('.header-fixed').removeClass('fixed');
        }
    });

    // Menu links scroll
    var lastId,
        topMenu = $("#top-menu, #right-nav"),
        topMenuHeight = topMenu.outerHeight(),
        menuItems = topMenu.find("a"),
        scrollItems = menuItems.map(function(){
          var item = $($(this).attr("href"));
          if (item.length) { return item; }
        });
    menuItems.click(function(e){
      var href = $(this).attr("href"),
          offsetTop = href === "#" ? 0 : $(href).offset().top-100;
      $('html, body').stop().animate({
          scrollTop: offsetTop
      }, 500);
      e.preventDefault();
    });
    $(window).scroll(function(){
       if($(window).scrollTop() == $(document).height() - $(window).height()) {
         var fromTop = $('#contacts').scrollTop()+50000;
         console.log('End');
       } else {
         var fromTop = $(this).scrollTop()+200;
       }
       var cur = scrollItems.map(function(){
         if ($(this).offset().top < fromTop)
           return this;
       });
       cur = cur[cur.length-1];
       var id = cur && cur.length ? cur[0].id : "";
       if (lastId !== id) {
           lastId = id;
           menuItems
             .parent().removeClass("active")
             .end().filter("[href='#"+id+"']").parent().addClass("active");
       }
    });

    // Background animation
    $(".first-bg").vegas({
        overlay: true,
        transition: 'fade',
        transitionDuration: 4000,
        delay: 10000,
        color: 'red',
        animation: 'random',
        animationDuration: 20000,
        slides: [
            { src: 'img/bg/main_bg2.jpg' },
            { src: 'img/bg/main_bg3.jpg' },
            { src: 'img/bg/main_bg4.jpg' },
            { src: 'img/bg/main_bg5.jpg' },
        ]
    });

    // Works
    var iso = new Isotope( '.grid', {
        itemSelector: '.element-item',
        layoutMode: 'fitRows'
      });

    var filterFns = {
        numberGreaterThan50: function( itemElem ) {
            var number = itemElem.querySelector('.number').textContent;
            return parseInt( number, 10 ) > 50;
        },
    ium: function( itemElem ) {
            var name = itemElem.querySelector('.name').textContent;
            return name.match( /ium$/ );
        }
    };

    var filtersElem = document.querySelector('.filters-button-group');
    filtersElem.addEventListener( 'click', function( event ) {
    if ( !matchesSelector( event.target, 'button' ) ) {
        return;
    }
    var filterValue = event.target.getAttribute('data-filter');
        filterValue = filterFns[ filterValue ] || filterValue;
        iso.arrange({ filter: filterValue });
    });

    var buttonGroups = document.querySelectorAll('.button-group');
    for ( var i=0, len = buttonGroups.length; i < len; i++ ) {
        var buttonGroup = buttonGroups[i];
        radioButtonGroup( buttonGroup );
    }

    function radioButtonGroup( buttonGroup ) {
        buttonGroup.addEventListener( 'click', function( event ) {
            if ( !matchesSelector( event.target, 'button' ) ) {
                return;
            }
            buttonGroup.querySelector('.active').classList.remove('active');
            event.target.classList.add('active');
        });
    }
    $('.grid').isotope({
        itemSelector: '.element-item',
        percentPosition: true,
    })

    // Parallax
$(function () {

    // Preloader
    // var $preloader = $('#page-preloader'),
    //     $spinner   = $preloader.find('.spinner');
    // $spinner.fadeOut();
    // $preloader.delay(350).fadeOut('slow');

    /* Параллакс от движения мыши */
    if ($(window).width() > 720)
    {
        $('body').parallax({
            'elements': [
                {
                    'selector': '.user1',
                    'properties': {
                        'x': {
                            'left': {
                                'initial': -5,
                                'multiplier': 0.005,
                                'unit': '%',
                                'invert': true
                            }
                        },
                        'y': {
                            'top': {
                                'initial': -10,
                                'multiplier': 0.005,
                                'unit': '%',
                                'invert': true
                            }
                        }
                    }
                },
                {
                    'selector': '.user2',
                    'properties': {
                      'x': {
                          'right': {
                              'initial': -25,
                              'multiplier': 0.007,
                              'unit': '%',
                              'invert': true
                          }
                      },
                      'y': {
                          'top': {
                              'initial': -10,
                              'multiplier': 0.007,
                              'unit': '%',
                              'invert': true
                          }
                      }
                  }
              },
              {
                'selector': '.user3',
                'properties': {
                    'x': {
                        'right': {
                            'initial': -30,
                            'multiplier': 0.009,
                            'unit': '%',
                            'invert': true
                        }
                    },
                    'y': {
                        'bottom': {
                            'initial': -30,
                            'multiplier': 0.009,
                            'unit': '%',
                            'invert': true
                        }
                    }
                }
            },
            {
                  'selector': '.user4',
                  'properties': {
                      'x': {
                          'right': {
                              'initial': -60,
                              'multiplier': 0.0011,
                              'unit': '%',
                              'invert': true
                          }
                      },
                      'y': {
                          'bottom': {
                              'initial': -60,
                              'multiplier': 0.0011,
                              'unit': '%',
                              'invert': true
                          }
                      }
                  }
              },
              {
                'selector': '.user5',
                'properties': {
                    'x': {
                        'left': {
                            'initial': -12,
                            'multiplier': 0.0013,
                            'unit': '%',
                            'invert': true
                        }
                    },
                    'y': {
                        'bottom': {
                            'initial': -12,
                            'multiplier': 0.002,
                            'unit': '%',
                            'invert': true
                        }
                    }
                }
            },
            ]
        });
    };
  });

  // Counter
    let time = 2, cc = 1;
    $(window).scroll(function() {
        let cPos = $('.why').offset().top,
            topWindow = $(window).scrollTop();
        if(cPos < topWindow + 100) {
            if(cc < 2) {
                $('.count').each(function () {
                    $(this).prop('Counter', 0).animate ( {
                        Counter:$(this).text()
                    }, {
                        duration: 4000,
                        easing: 'swing',
                        step: function(now) {
                            $(this).text(Math.ceil(now));
                            cc = cc + 2;
                        }
                    });
                });
            }
        }
    });
});
