import * as actions from "../actions/types";

const initialState = {
  stage: "contract",
  transaction: null,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case actions.PAYMENT_REPEAT:
      return initialState;

    case actions.PAYMENT_SUCCESS:
      return {
        ...state,
        stage: "success",
        transaction: payload,
        error: null
      };

    case actions.PAYMENT_FAILED:
      return {
        ...state,
        stage: "error",
        transaction: payload.transaction,
        error: payload.error
      };

    case actions.ACTIVE_CARD_CHANGED:
      return {
        ...state,
        stage: "contract",
        transaction: null,
        error: null
      };

    default:
      return state;
  }
};
