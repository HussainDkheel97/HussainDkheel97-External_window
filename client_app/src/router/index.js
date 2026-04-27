import Vue from "vue";
import VueRouter from "vue-router";
import Login from "../views/Auth/Login.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "login",
    component: Login,
  },

  {
    path: "/ex",
    name: "ex",
    // route level code-splitting
    // this generates a separate chunk (dashboard.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "dashboard" */
        "../views/external_sides/add_mail.vue"
      ),
  },

    {
    path: "/login_sides",
    name: "login_sides",
    // route level code-splitting
    // this generates a separate chunk (dashboard.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "dashboard" */
        "../views/external_sides/login_sides"
      ),
  },

     {
    path: "/my_mail",
    name: "my_mail",
    // route level code-splitting
    // this generates a separate chunk (dashboard.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "dashboard" */
        "../views/external_sides/my_mail"
      ),
  },
  

















  // inbox
 








  // sent




];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
