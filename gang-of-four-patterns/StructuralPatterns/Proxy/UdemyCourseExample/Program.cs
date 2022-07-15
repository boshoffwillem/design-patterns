/*
 * Notice that if you originally had `Car` here
 * you could just change it out because a Proxy
 * has the same API
 * */
ICar car = new CarProxy(new Driver { Age = 17 });
car.Drive();

var creature = new Creature
{
    Agility = 10
};
creature.Agility = 10;
